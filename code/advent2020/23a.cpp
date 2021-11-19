#include <bits/stdc++.h>
#include <gmpxx.h>
using namespace std;

template<class c> auto dud(c* x) -> decltype(cerr << *x, 0);
template<class c> char dud(...);
template<class c>struct iter{c b, e;};
template<class c>iter<c>iterate(c b,c e){return iter<c>{b,e};}
struct debug {
#ifdef DEBUG
  debug(){cerr<<boolalpha<<"  ";}
  ~debug(){cerr<<endl;}
  template<class c>typename enable_if<sizeof dud<c>(0)!=1,debug&>::type operator<<(c i){cerr<<i;return*this;}
  template<class c>typename enable_if<sizeof dud<c>(0)==1,debug&>::type operator<<(c i){return*this<<iterate(begin(i),end(i));}
  debug&operator<<(mpz_class i){cerr<<i.get_str();return*this;}
  template<class c,class b>debug&operator<<(pair<b,c>p){return*this<<"("<<p.first<<", "<<p.second<<")";}
  template<class c>debug&operator<<(iter<c>i){*this<<"(";for(auto it=i.b;it!=i.e;++it)*this<<", "+2*(it==i.b)<<*it;return*this<<")";}
#else
  template<class c>debug&operator<<(const c&){return*this;}
#endif  
};
#define pp(...) " [" << #__VA_ARGS__ ": " << (__VA_ARGS__) << "] "
typedef mpz_class ii;
typedef mpf_class id;
typedef mpq_class ir;
typedef ulong ul;

const deque<int> INPUT = {3,6,2,9,8,1,7,5,4};
const deque<int> INPUT_1 = {3,8,9,1,2,5,4,6,7};

void exec_move(deque<int> *cups) {
  int v = *cups->begin();
  vector<int> removed;
  while (removed.size() < 3) {
    removed.push_back(cups->at(1));
    cups->erase(cups->begin()+1);
  }
  int d = v-1;
  if (d == 0) d += 9;
  while (find(removed.begin(), removed.end(), d) != removed.end()) {
    d--;
    if (d == 0) d += 9;
  }
  const auto di = find(cups->begin(), cups->end(), d) + 1;
  cups->insert(di, removed.begin(), removed.end());
  cups->pop_front();
  cups->push_back(v);
}

int main() {
  deque<int> cups(INPUT);
  debug() << pp(cups);
  for (int i = 0; i < 100; ++i) {
    exec_move(&cups);
    debug() << pp(cups);
  }
  const auto i1 = find(cups.begin(), cups.end(), 1);
  for (auto it = i1+1; it != cups.end(); ++it) {
    cout << *it;
  }
  for (auto it = cups.begin(); it != i1; ++it) {
    cout << *it;
  }
  cout << endl;
  return 0;
}

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


int main() {
  vector<vector<string>> groups;
  vector<string> group;
  string l;
  while (getline(cin, l)) {
    if (l.length() == 0) {
      groups.push_back(vector<string>(group));
      group.clear();
      continue;
    }

    group.push_back(l);
  }
  groups.push_back(group);

  int count = 0;
  for (auto g : groups) {
    map<char,int> gc;
    for (auto a : g) {
      for (auto c : a) {
        gc[c]++;
      }
    }
    debug() << pp(gc);
    for (auto c : gc) {
      if (c.second == (int)g.size())
        ++count;
    }
  }
  cout << count << endl;
  return 0;
}

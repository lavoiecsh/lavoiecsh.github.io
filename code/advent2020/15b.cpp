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

const vector<int> TEST_INPUT_1 = {0,3,6};
const vector<int> TEST_INPUT_2 = {1,3,2};
const vector<int> TEST_INPUT_3 = {2,1,3};
const vector<int> TEST_INPUT_4 = {1,2,3};
const vector<int> TEST_INPUT_5 = {2,3,1};
const vector<int> TEST_INPUT_6 = {3,2,1};
const vector<int> TEST_INPUT_7 = {3,1,2};
const vector<int> INPUT = {2,0,1,9,5,19};

int main() {
  vector<int> initial(INPUT);
  map<size_t, size_t> numbers;
  for (size_t i = 0; i < initial.size(); ++i)
    numbers[initial[i]] = i;

  size_t last = initial[initial.size()-1];
  for (size_t i = initial.size(); i < 30000000; ++i) {
    auto f = numbers.find(last);
    size_t tmp = last;
    if (f == numbers.end()) {
      debug() << pp(i) pp(last) pp(0);
      last = 0;
    }
    else {
      debug() << pp(i) pp(last) pp(f->second);
      last = i-1- f->second;
    }
    numbers[tmp] = i-1;
  }

  cout << last << endl;
  
  return 0;
}

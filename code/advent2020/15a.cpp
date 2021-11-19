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
  vector<int> numbers(INPUT);
  for (size_t i = numbers.size(); i < 2020; ++i) {
    int last = numbers[i-1];
    size_t index = SIZE_MAX;
    for (size_t j = i - 2; j != SIZE_MAX; --j) {
      if (numbers[j] == last) {
        index = j;
        break;
      }
    }
    if (index == SIZE_MAX)
      numbers.push_back(0);
    else
      numbers.push_back(i-1-index);
  }

  cout << numbers[2019] << endl;
  
  return 0;
}

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

int counts(vector<int> split) {
  switch (split.size()) {
  case 1:
  case 2:
    return 1;
  case 3:
    return 2;
  case 4:
    return 4;
  case 5:
    return 7;
  default:
    return 0;
  }
}

int main() {
  vector<int> joltages;
  string line;
  while (getline(cin, line)) {
    joltages.push_back(stoi(line));
  }
  joltages.push_back(0);
  sort(joltages.begin(), joltages.end());
  joltages.push_back(joltages.back()+3);

  debug() << pp(joltages);
  vector<vector<int>> splits;
  size_t ps = 0;
  for (size_t i = 1; i < joltages.size(); ++i) {
    if (joltages[i] - joltages[i-1] == 3) {
      splits.push_back(vector<int>(joltages.begin()+ps, joltages.begin()+i));
      ps = i;
    }
  }
  debug() << pp(splits);

  long total = 1;
  for (auto s : splits) {
    total *= counts(s);
  }
  cout << total << endl;
  return 0;
}
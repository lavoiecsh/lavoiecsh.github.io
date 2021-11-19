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
  vector<string> map;
  string line;
  while (getline(cin, line)) map.push_back(line);

  int slope11 = 0, slope31 = 0, slope51 = 0, slope71 = 0, slope12 = 0;
  int y11 = 0, y31 = 0, y51 = 0, y71 = 0, y12 = 0;
  int ln = 0;
  for (auto l : map) {
    int s = l.size();
    if (l[y11 % s] == '#') ++slope11;
    y11 += 1;
    if (l[y31 % s] == '#') ++slope31;
    y31 += 3;
    if (l[y51 % s] == '#') ++slope51;
    y51 += 5;
    if (l[y71 % s] == '#') ++slope71;
    y71 += 7;
    if (ln++ % 2 == 0) {
      if (l[y12 % s] == '#') ++slope12;
      y12 += 1;
    }
  }

  cout << ii(slope11) * slope31 * slope51 * slope71 * slope12 << endl;
  
  return 0;
}

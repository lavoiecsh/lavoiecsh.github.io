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


typedef pair<string,string> field;
typedef vector<field> passport;
const vector<string> types = {"byr","iyr","eyr","hgt","hcl","ecl","pid"};

int main() {
  vector<passport> passports;
  string line, f;
  passport p;
  while(getline(cin, line)) {
    if (line.length() == 0) {
      passports.push_back(passport(p));
      p.clear();
      continue;
    }
    stringstream l(line);
    while(getline(l, f, ' ')) {
      p.push_back({f.substr(0, 3), f.substr(4)});
    }
  }
  passports.push_back(p);

  int count = 0;
  for (auto p2 : passports) {
    if (p2.size() < 7) continue;
    if (p2.size() == 8) {
      ++count;
      continue;
    }
    bool z = true;
    for (auto f2 : p2) {
      if (f2.first == "cid") {
        z = false;
        break;
      }
    }
    if (z) {
      ++count;
    }
  }
  cout << count << endl;
  return 0;
}

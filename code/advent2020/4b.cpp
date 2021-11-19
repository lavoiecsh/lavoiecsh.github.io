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
  vector<string> ecls = {"amb","blu","brn","gry","grn","hzl","oth"};
  regex hgtr("(\\d+)(cm|in)");
  smatch hgtm;
  regex hclr("#[0-9a-z]{6}");
  for (auto p : passports) {
    debug() << pp(p);
    if (p.size() < 7) continue;
    int c = 0;
    for (auto f : p) {
      debug() << pp(f);
      if (f.first == "byr") {
        int x = stoi(f.second);
        if (x >= 1920 && x <= 2002) {++c;debug() << "passed";}
      } else if (f.first == "iyr") {
        int x = stoi(f.second);
        if (x >= 2010 && x <= 2020) {++c;debug() << "passed";}
      } else if (f.first == "eyr") {
        int x = stoi(f.second);
        if (x >= 2020 && x <= 2030) {++c;debug() << "passed";}
      } else if (f.first == "hgt") {
        if (regex_match(f.second, hgtm, hgtr)) {
          int x = stoi(hgtm[1]);
          if (hgtm[2] == "cm" && x >= 150 && x <= 193) {++c;debug() << "passed";}
          if (hgtm[2] == "in" && x >= 59 && x <=76) {++c;debug() << "passed";}
        }
      } else if (f.first == "hcl") {
        if (regex_match(f.second, hclr)) {++c;debug() << "passed";}
      } else if (f.first == "ecl") {
        if (find(ecls.begin(), ecls.end(), f.second) != ecls.end()) {++c;debug() << "passed";}
      } else if (f.first == "pid") {
        if (f.second.length() == 9) {++c;debug() << "passed";}
      }
    }
    debug() << pp(c);
    if (c == 7) ++count;
  }
  cout << count << endl;
  return 0;
}

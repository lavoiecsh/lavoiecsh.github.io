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

map<string, map<string,int>> conversions;
int count(string key) {
  debug() << pp(key) pp(conversions[key]);
  if (conversions[key].size() == 0) return 1;
  int z = 0;
  for (auto c : conversions[key]) {
    z += c.second * count(c.first);
  }
  debug() << pp(key) pp(conversions[key]) pp(z);
  return z + 1;
}

int main() {
  string line;
  regex r("(\\w+ \\w+) bags contain (.*)");
  regex r2("(\\d+) (\\w+ \\w+) bags?[,.][ ]?(.*)");
  smatch m, m2;
  while (getline(cin, line)) {
    regex_match(line, m, r);
    string key = m[1];
    string rest = m[2];
    map<string,int> c;
    while (regex_match(rest, m2, r2)) {
      c.insert({m2[2], stoi(m2[1])});
      rest = m2[3];
    }
    conversions.insert({key,c});
  }

  cout << count("shiny gold") - 1 << endl;
  return 0;
}
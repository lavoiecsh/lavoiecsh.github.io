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

typedef struct Password {
  int min, max;
  char letter;
  string password;
} Password;


int main() {
  regex r("(\\d+)-(\\d+) (\\w): (\\w+)");
  vector<Password> passwords;
  string line;
  smatch sm;
  while (getline(cin, line)) {
    regex_match(line, sm, r);
    Password p;
    p.min = stoi(sm[1].str());
    p.max = stoi(sm[2].str());
    p.letter = sm[3].str()[0];
    p.password = sm[4].str();
    passwords.push_back(p);
  }

  int count = 0;
  for (auto p : passwords) {
    int lc = 0;
    for (auto c : p.password) {
      if (c == p.letter) ++ lc;
    }
    if (p.min <= lc && lc <= p.max) ++count;
  }
  cout << count << endl;
  
  return 0;
}

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

pair<ii,ii> repetition(pair<ii,ii> rep, pair<int,long> bus) {
  ii first = 0;
  for (ii x = rep.first; ; x += rep.second) {
    if ((x + bus.first) % bus.second != 0) continue;
    if (first == 0) {
      first = x;
      continue;
    }
    return {first,x-first};
  }
}

int main() {
  string line;
  getline(cin, line);
  vector<pair<int,long>> buses;
  int number = -1;
  while (getline(cin, line, ',')) {
    ++number;
    if (line == "x") continue;
    buses.push_back({number,stol(line)});
  }

  pair<ii,ii> rep = buses[0];
  for (auto b : buses) {
    if (b == buses[0]) continue;
    rep = repetition(rep, b);
    debug() << pp(b) pp(rep);
  }
  cout << rep.first << endl;
  
  return 0;
}

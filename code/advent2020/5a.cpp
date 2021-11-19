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

int avg(int a, int b) {
  return (a + b) / 2;
}

int calculate_pass(string pass) {
  int ra = 0, rb = 127;
  for (int i = 0; i < 7; ++i) {
    if (pass[i] == 'F') {
      rb = avg(ra,rb);
    } else {
      ra = avg(ra,rb);
    }
  }

  int ca = 0, cb = 7;
  for (int i = 7; i < 10; ++i) {
    if (pass[i] == 'L') {
      cb = avg(ca,cb);
    } else {
      ca = avg(ca,cb);
    }
  }

  debug() << pp(ra) pp(rb) pp(ca) pp(cb);

  return rb*8+cb;
}


int main() {
  vector<string> passes;
  string pass;
  while(getline(cin, pass)) {
    passes.push_back(pass);
  }

  debug() << calculate_pass("FBFBBFFRLR");
  
  int max = 0;
  for (auto p : passes) {
    int x = calculate_pass(p);
    if (x > max) max = x;
  }

  cout << max << endl;
  
  return 0;
}

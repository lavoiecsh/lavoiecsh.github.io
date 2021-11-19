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

long simple_evaluate(string expression) {
  long value = 0;
  stringstream ss(expression);
  string e;
  char op = ' ';
  while (getline(ss, e, ' ')) {
    switch (e[0]) {
    case '+':
    case '*':
      op = e[0];
      break;
    default:
      switch (op) {
      case ' ':
        value = stol(e);
        break;
      case '+':
        value += stol(e);
        break;
      case '*':
        value *= stol(e);
        break;
      }
      break;
    }
  }
  return value;
}

regex parens_r(".*(\\([^\\)]+\\)).*");
smatch parens_m;
long evaluate(string expression) {
  while (regex_match(expression, parens_m, parens_r)) {
    debug() << pp(parens_m[1]);
    long subValue = simple_evaluate(parens_m[1].str().substr(1, parens_m[1].length()-2));
    expression.replace(expression.find(parens_m[1]), parens_m[1].length(), to_string(subValue));
    debug() << pp(expression);
  }
  return simple_evaluate(expression);
}

int main() {
  long total = 0;
  string line;
  while (getline(cin, line)) {
    debug() << pp(line);
    long v = evaluate(line);
    debug() << pp(v);
    total += v;
  }

  cout << total << endl;
  
  return 0;
}

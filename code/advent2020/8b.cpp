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

vector<pair<string,int>> operations;
pair<bool,int> run() {
  set<int> seen;
  int acc = 0;
  int inst = 0;
  do {
    seen.insert(inst);
    switch (operations[inst].first[0]) {
    case 'n':
      inst++;
      break;
    case 'a':
      acc += operations[inst].second;
      inst++;
      break;
    case 'j':
      inst += operations[inst].second;
      break;
    }
    if ((size_t)inst == operations.size()) {
      return {true,acc};
    }
  } while(seen.find(inst) == seen.end());
  return {false,acc};
}

int main() {
  string line;
  while (getline(cin, line)) {
    operations.push_back({ line.substr(0, 3), stoi(line.substr(4) )});
  }

  for (size_t i = 0; i < operations.size(); ++i) {
    if (operations[i].first == "nop") {
      operations[i].first = "jmp";
      auto r = run();
      if (r.first) {
        cout << r.second << endl;
        return 0;
      }
      operations[i].first = "nop";
    } else if (operations[i].first == "jmp") {
      operations[i].first = "nop";
      auto r = run();
      if (r.first) {
        cout << r.second << endl;
        return 0;
      }
      operations[i].first = "jmp";
    }
  }
  return 0;
}

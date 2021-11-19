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

regex reg("([^:]+): (\\d+)-(\\d+) or (\\d+)-(\\d+)");
class Rule {
public:
  Rule(string line) {
    smatch sm;
    regex_match(line, sm, reg);
    this->name = sm[1];
    this->bounds1 = {stoi(sm[2]), stoi(sm[3])};
    this->bounds2 = {stoi(sm[4]), stoi(sm[5])};
  }

  bool invalid(int n) {
    return n < bounds1.first ||
      (n > bounds1.second && n < bounds2.first) ||
      n > bounds2.second;
  }

  string name;
  pair<int,int> bounds1;
  pair<int,int> bounds2;
};

vector<Rule> rules;
int calc(vector<int> ticket) {
  int total = 0;
  debug() << pp(ticket);
  for (auto n : ticket) {
    debug() << pp(n);
    bool v = false;
    for (auto r : rules) {
      if (!r.invalid(n)) {
        debug() << pp(r.bounds1) pp(r.bounds2);
        v = true;
        break;
      }
    }
    if (!v) {
      total += n;
    }
  }
  return total;
}

int main() {
  vector<int> ticket;
  vector<vector<int>> nearby;
  string line, tmp;
  bool nearbyTickets = false;
  while (getline(cin, line)) {
    if (line.length() == 0) continue;
    if (line[0] == 'y') {
      getline(cin, line);
      stringstream l(line);
      while (getline(l, tmp, ',')) 
        ticket.push_back(stoi(tmp));
      continue;
    }
    if (line[0] == 'n') {
      nearbyTickets = true;
      continue;
    }
    if (nearbyTickets) {
      vector<int> n;
      stringstream l(line);
      while (getline(l, tmp, ','))
        n.push_back(stoi(tmp));
      nearby.push_back(n);
      continue;
    }
    rules.push_back(Rule(line));
  }

  int total = 0;
  for (auto t : nearby) {
    total += calc(t);
  }

  cout << total << endl;
  
  return 0;
}
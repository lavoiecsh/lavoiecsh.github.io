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

map<string,int> counts;
map<string,set<string>> possible;

void update_possible(string allergen, set<string> allergens) {
  auto found = possible.find(allergen);
  if (found == possible.end()) {
    possible.insert({allergen, set<string>(allergens)});
    return;
  }
  set<string> n;
  for (auto a : found->second) {
    if (allergens.find(a) != allergens.end()) {
      n.insert(a);
    }
  }
  possible[allergen] = n;
}

int main() {
  string line;
  regex r("(.*) \\(contains (.*)\\)");
  smatch sm;
  while (getline(cin, line)) {
    regex_match(line, sm, r);
    set<string> ingredients;
    stringstream ss(sm[1]);
    while (getline(ss, line, ' ')) {
      ingredients.insert(line);
      counts[line]++;
    }
    set<string> allergens;
    ss = stringstream(sm[2]);
    while (getline(ss, line, ',')) {
      string allergen = line;
      if (line[0] == ' ') {
        allergen = allergen.substr(1);
      }
      allergens.insert(allergen);
      update_possible(allergen, ingredients);
    }
  }

  debug() << pp(counts);
  debug() << pp(possible);

  ul total = 0;
  for (auto c : counts) {
    bool n = true;
    for (auto p : possible) {
      if (p.second.find(c.first) != p.second.end()) {
        n = false;
        break;
      }
    }
    if (n) {
      total += c.second;
    }
  }
  cout << total << endl;
  
  return 0;
}

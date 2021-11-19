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

regex rule_or_r("(.+) \\| (.*)");
regex rule_and_r("(\\d+) (\\d+)");
regex rule_simple_r("(\\d+)");
regex rule_char_r("\"(\\w)\"");
class Rule {
public:
  virtual ~Rule() {};
  virtual bool matches(string input) = 0;
  virtual set<string> possibilities() = 0;
};
map<int, Rule*> rules;

class CharRule : public Rule {
public:
  CharRule(string def) {
    this->c = def[0];
  }
  bool matches(string input) {
    return input.length() == 1 && input == this->c;
  }
  set<string> possibilities() {
    return {c};
  }
  string c;
};

class SingleRule : public Rule {
public:
  SingleRule(string def) {
    this->rule_number = stoi(def);
  }
  bool matches(string input) {
    return rules[rule_number]->matches(input);
  }
  set<string> possibilities() {
    return rules[rule_number]->possibilities();
  }
  int rule_number;
};

class AndRule : public Rule {
public:
  AndRule(string left, string right) {
    smatch sm;
    rule1 = stoi(left);
    rule2 = stoi(right);
  }
  bool matches(string input) {
    debug() << pp(rule1) pp(rule2) pp(input);
    for (size_t i = 1; i < input.length(); ++i) {
      if (rules[rule1]->matches(input.substr(0, i)) && rules[rule2]->matches(input.substr(i))) {
        return true;
      }
    }
    return false;
  }
  set<string> possibilities() {
    set<string> left = rules[rule1]->possibilities();
    set<string> right = rules[rule2]->possibilities();
    set<string> all;
    for (auto l : left) {
      for (auto r : right) {
        all.insert(l + r);
      }
    }
    return all;
  }
  int rule1, rule2;
};

class OrRule : public Rule {
public:
  OrRule(string left, string right) {
    smatch sm;
    if (regex_match(left, sm, rule_and_r)) {
      this->rule1 = new AndRule(sm[1].str(), sm[2].str());
    } else {
      this->rule1 = new SingleRule(left);
    }
    if (regex_match(right, sm, rule_and_r)) {
      this->rule2 = new AndRule(sm[1].str(), sm[2].str());
    } else {
      this->rule2 = new SingleRule(right);
    }
  }
  ~OrRule() {
    delete(this->rule1);
    delete(this->rule2);
  }
  bool matches(string input) {
    return rule1->matches(input) || rule2->matches(input);
  }
  set<string> possibilities() {
    set<string> left = rule1->possibilities();
    set<string> right = rule2->possibilities();
    set<string> all(left);
    all.insert(right.begin(), right.end());
    return all;
  }
  Rule *rule1, *rule2;
};

regex rule_number_r("(\\d+): (.*)");
int main() {
  vector<string> inputs;
  string line;
  smatch sm;
  while(getline(cin, line)) {
    if (line.length() == 0) continue;
    if (regex_match(line, sm, rule_number_r)) {
      int rule_number = stoi(sm[1].str());
      string rest = sm[2].str();
      if (regex_match(rest, sm, rule_or_r)) {
        rules.insert({rule_number, new OrRule(sm[1], sm[2])});
      } else if (regex_match(rest, sm, rule_and_r)) {
        rules.insert({rule_number, new AndRule(sm[1], sm[2])});
      } else if (regex_match(rest, sm, rule_char_r)) {
        rules.insert({rule_number, new CharRule(sm[1].str())});
      } else {
        rules.insert({rule_number, new SingleRule(rest)});
      }
      continue;
    }

    inputs.push_back(line);
  }

  set<string> p42 = rules[42]->possibilities();
  set<string> p31 = rules[31]->possibilities();

  ostringstream ss42;
  copy(p42.begin(), p42.end(), ostream_iterator<string>(ss42, "|"));
  string s42 = ss42.str();
  s42.pop_back();
  s42.insert(0, "(");
  s42.push_back(')');
  debug() << pp(s42);

  ostringstream ss31;
  copy(p31.begin(), p31.end(), ostream_iterator<string>(ss31, "|"));
  string s31 = ss31.str();
  s31.pop_back();
  s31.insert(0, "(");
  s31.push_back(')');
  debug() << pp(s31);

  ostringstream ss4231;
  for (auto x42 : p42) {
    for (auto x31 : p31) {
      ss4231 << x42 << x31 << "|";
    }
  }
  string s4231 = ss4231.str();
  s4231.pop_back();
  s4231.insert(0, "(");
  s4231.push_back(')');
  debug() << pp(s4231);

  string s8 = s42 + "+";
  debug() << pp(s8);
  string s11 = s42 + "(" + s42 + s31 + ")*" + s31;
  // s11 = s42 + s4231 + "*" + s31;
  // s11 = s42 + s31
  s11 = s42 + "{x}" + s31 + "{x}";
  debug() << pp(s11);;
  string s0 = "^" + s8 + s11 + "$";
  debug() << pp(s0);
  // regex r0(s0);

  vector<regex> r0s;
  for (int i = 1; i < 10; ++i) {
    r0s.push_back(regex("^" + s8 + s42 + "{" + to_string(i) + "}" + s31 + "{" + to_string(i) + "}$"));
  }
  
  int count = 0;
  for (auto i : inputs) {
    for (auto r : r0s) {
      if (regex_search(i, r)) {
        debug() << pp(i) "matched";
        ++count;
        break;
      }
    }
  }
  cout << count << endl;

  for (auto r : rules) {
    delete(r.second);
  }
  return 0;
}

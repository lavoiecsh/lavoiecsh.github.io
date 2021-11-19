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

vector<string> plan;
int count(size_t row, size_t col, char c) {
  int x = 0;
  if (plan[row-1][col-1] == '#') ++x;
  if (plan[row-1][col] == '#') ++x;
  if (plan[row-1][col+1] == '#') ++x;
  if (plan[row][col-1] == '#') ++x;
  if (plan[row][col+1] == '#') ++x;
  if (plan[row+1][col-1] == '#') ++x;
  if (plan[row+1][col] == '#') ++x;
  if (plan[row+1][col+1] == '#') ++x;
  return x;
}

char calculate(size_t row, size_t col) {
  switch (plan[row][col]) {
  case 'L':
    if (count(row, col, '#') == 0) return '#';
    return 'L';
  case '#':
    if (count(row, col, '#') >= 4) return 'L';
    return '#';
  default:
    return '.';
  }
}

int iterate() {
  int count = 0;
  vector<string> plan2;
  for (size_t row = 0; row < plan.size(); ++row) {
    string line2;
    for (size_t col = 0; col < plan[row].size(); ++col) {
      if (row == 0 || col == 0 || row == plan.size()-1 || col == plan[row].size()-1) {
        line2 += ".";
        continue;
      }
      char x = calculate(row, col);
      if (x != plan[row][col]) ++count;
      line2 += x;
    }
    plan2.push_back(line2);
  }
  plan = plan2;
  return count;
}

int main() {
  string line;
  while (getline(cin, line)) {
    plan.push_back("." + line + ".");
  }
  string outside;
  for (size_t i = 0; i < plan[0].length(); ++i) {
    outside += ".";
  }
  plan.insert(plan.begin(), outside);
  plan.push_back(outside);

  while (iterate() != 0);

  int count = 0;
  for (size_t row = 0; row < plan.size(); ++row) {
    for (size_t col = 0; col < plan[row].size(); ++col) {
      if (plan[row][col] == '#') ++count;
    }
  }

  cout << count << endl;
  
  return 0;
}

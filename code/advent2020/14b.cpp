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

string applyMask(string mask, long number) {
  debug() << pp(mask);
  string n;
  while (number > 0) {
    n += number % 2 == 0 ? '0' : '1';
    number /= 2;
  }
  size_t length = mask.length();
  while (n.length() < length) {
    n += '0';
  }
  debug() << pp(n);
  for (size_t i = 0; i < length; ++i) {
    if (mask[i] == '0') continue;
    n[length-i-1] = mask[i];
  }
  debug() << pp(n);
  return n;
}

vector<long> generateAddresses(string mask, long address) {
  string number = applyMask(mask, address);
  vector<long> addresses = {0};
  long power = 1;
  for (auto d : number) {
    if (d == '1') {
      for (size_t i = 0; i < addresses.size(); ++i) {
        addresses[i] += power;
      }
    }
    if (d == 'X') {
      vector<long> addresses2(addresses);
      for (size_t i = 0; i < addresses2.size(); ++i) {
        addresses2[i] += power;
      }
      addresses.insert(addresses.end(), addresses2.begin(), addresses2.end());
    }
    power *= 2;
  }
  return addresses;
}

int main() {
  string line;
  map<long,long> memory;
  string mask;
  regex memRegex("mem\\[(\\d+)\\] = (\\d+)");
  smatch sm;
  while (getline(cin, line)) {
    if (regex_match(line, sm, memRegex)) {
      long address = stoi(sm[1]);
      long value = stoi(sm[2]);
      vector<long> addresses = generateAddresses(mask, address);
      debug() << pp(addresses);
      for (auto a : addresses) {
        memory[a] = value;
      }
      continue;
    }
    mask = line.substr(7);
  }
  debug() << pp(memory);
  long sum = 0;
  for (auto m : memory) {
    sum += m.second;
  }
  cout << sum << endl;
  return 0;
}

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


const ul mod = 20201227;
const ul subject_number = 7;

const pair<ul,ul> input_1 = {5764801,17807724};
const pair<ul,ul> input = {12232269,19452773};

ul find_loop_size(ul pk) {
  ul v = 1;
  ul ls = 0;
  while (v != pk) {
    v *= subject_number;
    v %= mod;
    ++ls;
  }
  return ls;
}

ul find_encryption_key(ul pk, ul ls) {
  ul v = 1;
  while (ls-- != 0) {
    v *= pk;
    v %= mod;
  }
  return v;
}

int main() {
  auto [pk_car,pk_door] = input;
  ul ls_car = find_loop_size(pk_car);
  ul ls_door = find_loop_size(pk_door);
  ul ek_car = find_encryption_key(pk_car, ls_door);
  ul ek_door = find_encryption_key(pk_door, ls_car);
  debug() << pp(pk_car) pp(ls_car) pp(ek_car);
  debug() << pp(pk_door) pp(ls_door) pp(ek_door);

  cout << ek_car << endl;
  return 0;
}

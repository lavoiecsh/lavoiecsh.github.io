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

set<pair<int,int>> flipped;

pair<int,int> coordinates(string line) {
  pair<int,int> c = {0,0};
  for (ul i = 0; i < line.length(); ++i) {
    switch(line[i]) {
    case 'e':
      c.first++;
      break;
    case 'w':
      c.first--;
      break;
    case 'n':
      c.second--;
      ++i;
      if (line[i] == 'e') c.first++;
      break;
    case 's':
      c.second++;
      ++i;
      if (line[i] == 'w') c.first--;
      break;
    }
  }
  return c;
}

pair<pair<int,int>,pair<int,int>> maximums() {
  pair<int,int> qs = {INT_MAX,INT_MIN};
  pair<int,int> rs = {INT_MAX,INT_MIN};
  for (auto f : flipped) {
    int q = f.first;
    if (q < qs.first) qs.first = q;
    if (q > qs.second) qs.second = q;
    int r = f.second;
    if (r < rs.first) rs.first = r;
    if (r > rs.second) rs.second = r;
  }
  return {qs,rs};
}

int flipped_neighbours(int q, int r) {
  int count = 0;
  if (flipped.find({q+1,r}) != flipped.end()) ++count;
  if (flipped.find({q-1,r}) != flipped.end()) ++count;
  if (flipped.find({q,r+1}) != flipped.end()) ++count;
  if (flipped.find({q,r-1}) != flipped.end()) ++count;
  if (flipped.find({q+1,r-1}) != flipped.end()) ++count;
  if (flipped.find({q-1,r+1}) != flipped.end()) ++count;
  return count;
}

void life() {
  auto [qs,rs] = maximums();
  // debug() << pp(flipped);
  set<pair<int,int>> next;
  for (int q = qs.first-1; q <= qs.second+1; ++q) {
    for (int r = rs.first-1; r <= rs.second+1; ++r) {
      pair<int,int> c = {q,r};
      int count = flipped_neighbours(q, r);
      auto f = flipped.find(c);
      if (f != flipped.end()) {
        // debug() << "black" pp(count) pp(c);
        if (count == 0 || count > 2) {
          // debug() << "flipping";
        } else {
          next.insert({q,r});
        }
      } else {
        // debug() << "white" pp(count) pp(q) pp(r);
        if (count == 2) {
          // debug() << "flipping";
          next.insert({q,r});
        }
      }
    }
  }
  flipped = next;
}

int main() {
  string line;
  while (getline(cin, line)) {
    auto coord = coordinates(line);
    auto f = flipped.find(coord);
    if (f == flipped.end())
      flipped.insert(coord);
    else
      flipped.erase(f);
  }

  debug() << pp(0) pp(flipped.size());
  for (int i = 0; i < 100; ++i) {
    life();
    debug() << pp(i) pp(flipped.size());
  }

  cout << flipped.size() << endl;
  
  return 0;
}

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
  template<class c,class b,class a>debug&operator<<(tuple<a,b,c>p){auto[x,y,z]=p;return*this<<"("<<x<<", "<<y<<", "<<z<<")";}
  template<class c,class b,class a,class d>debug&operator<<(tuple<a,b,c,d>p){auto[x,y,z,w]=p;return*this<<"("<<x<<", "<<y<<", "<<z<<", "<<w<<")";}
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

typedef tuple<int,int,int,int> coord;
typedef tuple<pair<int,int>,pair<int,int>,pair<int,int>,pair<int,int>> extremities;
set<coord> actives;

int active_neighbours(coord c) {
  auto [wi,zi,yi,xi] = c;
  int count = 0;
  for (int w = wi-1; w <= wi+1; ++w) {
    for (int z = zi-1; z <= zi+1; ++z) {
      for (int y = yi-1; y <= yi+1; ++y) {
        for (int x = xi-1; x <= xi+1; ++x) {
          if (x == xi && y == yi && z == zi && w == wi) continue;
          if (actives.find({w,z,y,x}) != actives.end()) ++count;
        }
      }
    }
  }
  return count;
}

extremities extremes() {
  int minW = INT_MAX, minZ = INT_MAX, minY = INT_MAX, minX = INT_MAX;
  int maxW = INT_MIN, maxZ = INT_MIN, maxY = INT_MIN, maxX = INT_MIN;
  for (auto [w,z,y,x] : actives) {
    if (w < minW) minW = w;
    if (w > maxW) maxW = w;
    if (z < minZ) minZ = z;
    if (z > maxZ) maxZ = z;
    if (y < minY) minY = y;
    if (y > maxY) maxY = y;
    if (x < minX) minX = x;
    if (x > maxX) maxX = x;
  }
  return {{minW, maxW},{minZ,maxZ},{minY,maxY},{minX,maxX}};
}

void iterate() {
  set<coord> next;
  auto [we,ze,ye,xe] = extremes();
  for (int w = we.first-1; w <= we.second+1; ++w) {
    for (int z = ze.first-1; z <= ze.second+1; ++z) {
      for (int y = ye.first-1; y <= ye.second+1; ++y) {
        for (int x = xe.first-1; x <= xe.second+1; ++x) {
          coord c = {w,z,y,x};
          int an = active_neighbours(c);
          if (actives.find(c) != actives.end()) {
            if (an == 2 || an == 3) next.insert(c);
          } else {
            if (an == 3) next.insert(c);
          }
        }
      }
    }
  }
  actives = next;
}

void print() {
  auto [we,ze,ye,xe] = extremes();
  for (int w = we.first; w <= we.second; ++w) {
    for (int z = ze.first; z <= ze.second; ++z) {
      debug() << pp(w) pp(z);
      for (int y = ye.first; y <= ye.second; ++y) {
        string line;
        for (int x = xe.first; x <= xe.second; ++x) {
          line += actives.find({w,z,y,x}) == actives.end() ? '.' : '#';
        }
        debug() << line;
      }
    }
  }
}

int main() {
  vector<string> slice;
  string line;
  while (getline(cin, line)) {
    slice.push_back(line);
  }

  int y = -(slice.size()/2);
  for (auto l : slice) {
    int x = -(slice.size()/2);
    for (auto c : l) {
      if (c == '#') actives.insert({0,0,y,x});
      ++x;
    }
    ++y;
  }
  debug() << pp(actives);
  print();

  for (int i = 0; i < 6; ++i) {
    iterate();
    debug() << pp(actives);
    print();
  }

  cout << actives.size() << endl;
  
  return 0;
}

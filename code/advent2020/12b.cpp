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

int wx = 10;
int wy = 1;
int sx = 0;
int sy = 0;


void execute(pair<char, int> action) {
  switch (action.first) {
  case 'N':
    wy += action.second;
    break;
  case 'S':
    wy -= action.second;
    break;
  case 'E':
    wx += action.second;
    break;
  case 'W':
    wx -= action.second;
    break;
  case 'L':
  case 'R':
    {
      int degrees = action.second;
      if (action.first == 'R')
        degrees = 360 - degrees;
      switch (degrees) {
      case 90:
        {
          int t = wx;
          wx = -wy;
          wy = t;
        }
        break;
      case 180:
        wx *= -1;
        wy *= -1;
        break;
      case 270:
        {
          int t = wx;
          wx = wy;
          wy = -t;
        }
        break;
      default:
        break;
      }
      debug() << pp(action) pp(degrees) pp(pair<int,int>(wx,wy));
    }
    break;
  case 'F':
    sx += wx * action.second;
    sy += wy * action.second;
    break;
  }
}

int main() {
  vector<pair<char, int>> actions;
  string line;
  while (getline(cin, line)) {
    actions.push_back({ line[0], stoi(line.substr(1)) });
  }
  
  for (auto a : actions) {
    execute(a);
    debug() << pp(pair<int,int>(sx,sy)) pp(pair<int,int>(wx,wy));
  }

  cout << abs(sx) + abs(sy) << endl;
  
  return 0;
}

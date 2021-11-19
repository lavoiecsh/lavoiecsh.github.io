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


multimap<ul,ul> right_matches;
multimap<ul,ul> bottom_matches;
vector<vector<ul>> rows;
map<ul,vector<string>> tiles;
const ul TILE_SIZE = 9;
const ul TILING_SIZE = 12;

vector<string> orientate(ul orientation) {
  vector<string> start = tiles[orientation / 100];
  vector<string> rotated, flipped;
  switch (orientation / 10 % 10) {
  case 0:
    rotated = vector<string>(start);
    break;
  case 1:
    for (ul c = TILE_SIZE; c != SIZE_MAX; --c) {
      ostringstream l;
      for (ul r = 0; r <= TILE_SIZE; ++r) {
        l << start[r][c];
      }
      rotated.push_back(l.str());
    }
    break;
  case 2:
    for (ul r = TILE_SIZE; r != SIZE_MAX; --r) {
      stringstream l;
      for (ul c = TILE_SIZE; c != SIZE_MAX; --c) {
        l << start[r][c];
      }
      rotated.push_back(l.str());
    }
    break;
  case 3:
    for (ul c = 0; c <= TILE_SIZE; ++c) {
      stringstream l;
      for (ul r = TILE_SIZE; r != SIZE_MAX; --r) {
        l << start[r][c];
      }
      rotated.push_back(l.str());
    }
    break;
  }
  switch (orientation % 10) {
  case 0:
    flipped = vector<string>(rotated);
    break;
  case 1:
    for (ul r = TILE_SIZE; r != SIZE_MAX; --r) {
      flipped.push_back(rotated[r]);
    }
    break;
  case 2:
    for (auto r : rotated) {
      stringstream l;
      for (ul c = TILE_SIZE; c != SIZE_MAX; --c) {
        l << r[c];
      }
      flipped.push_back(l.str());
    }
    break;
  }
  return flipped;
}

pair<bool, bool> fits(ul a, ul b) {
  vector<string> oa = orientate(a);
  vector<string> ob = orientate(b);
  bool right = true;
  for (ul r = 0; r <= TILE_SIZE; ++r) {
    if (ob[r][0] != oa[r][TILE_SIZE]) {
      right = false;
      break;
    };
  }
  bool bottom = ob[0] == oa[TILE_SIZE];
  return {right,bottom};
}

ul h(ul t, ul rot, ul flip) {
  return t * 100 + rot * 10 + flip;
}

void save_matches(ul ta, ul tb) {
  for (int ra = 0; ra <= 3; ++ra) {
    for (int fa = 0; fa <= 2; ++fa) {
      ul a = h(ta,ra,fa);
      for (int rb = 0; rb <= 3; ++rb) {
        for (int fb = 0; fb <= 2; ++fb) {
          ul b = h(tb,rb,fb);
          auto f = fits(a,b);
          if (f.first) right_matches.insert({a,b});
          if (f.second) bottom_matches.insert({a,b});
        }
      }
    }
  }
}

void print(vector<string> tile) {
  for (ul r = 0; r < TILE_SIZE; ++r) {
    debug() << tile[r];
  }
}

typedef vector<ul> row;
vector<row> rowify_next(vector<ul> current) {
  vector<vector<ul>> nexts;
  for (auto it = right_matches.lower_bound(current[current.size()-1]); it != right_matches.upper_bound(current[current.size()-1]); ++it) {
    if (find(current.begin(), current.end(), it->second) != current.end()) continue;
    vector<ul> n(current);
    n.push_back(it->second);
    nexts.push_back(n);
  }
  return nexts;
}

vector<row> rowify(ul first) {
  vector<row> orientations;
  orientations.push_back({first});
  while (orientations[0].size() != TILING_SIZE) {
    vector<row> nexts = rowify_next(orientations[0]);
    orientations.insert(orientations.end(), nexts.begin(), nexts.end());
    orientations.erase(orientations.begin());
    if (orientations.size() == 0)
      return orientations;
  }
  return orientations;
}

vector<row> filter_used(set<ul> used, vector<row> rest) {
  vector<row> not_used;
  for (auto r : rest) {
    bool u = false;
    for (auto i : used) {
      for (auto j : r) {
        if (i == j / 100) {
          u = true;
          break;
        }
      }
      if (u) break;
    }
    if (!u) {
      not_used.push_back(row(r));
    }
  }
  return not_used;
}

bool fits_bottom(row top, row bottom) {
  for (ul i = 0; i != TILING_SIZE; ++i) {
    bool found = false;
    for (auto it = bottom_matches.lower_bound(top[i]); it != bottom_matches.upper_bound(top[i]); ++it) {
      if (it->second == bottom[i]) {
        found = true;
        break;
      }
    }
    if (!found) return false;
  }
  return true;
}

vector<row> find_nexts(set<ul> used, row current) {
  vector<row> nexts;
  vector<row> filtered = filter_used(used, rows);
  for (auto f : filtered) {
    if (fits_bottom(current, f)) {
      nexts.push_back(row(f));
    }
  }
  return nexts;
}

set<ul> add_seen(set<ul> prev, row r) {
  set<ul> next(prev);
  for (auto t : r) {
    next.insert(t / 100);
  }
  return next;
}

vector<row> add_row(vector<row> rs, row r) {
  vector<row> next(rs);
  next.push_back(r);
  return next;
}

vector<row> loop(set<ul> seen, row last, vector<row> rs) {
  if (rs.size() == TILING_SIZE) {
    return rs;
  }
  vector<row> nexts = find_nexts(seen, last);
  for (auto n : nexts) {
    vector<row> solution = loop(add_seen(seen, n), n, add_row(rs, n));
    if (solution.size() != 0) return solution;
  }
  return {};
}

vector<vector<pair<int,int>>> monsters_horizontal = {
  {{0,18},{1,0},{1,5},{1,6},{1,11},{1,12},{1,17},{1,18},{1,19},{2,1},{2,4},{2,7},{2,10},{2,13},{2,16}},
  {{2,18},{1,0},{1,5},{1,6},{1,11},{1,12},{1,17},{1,18},{1,19},{0,1},{0,4},{0,7},{0,10},{0,13},{0,16}},
  {{0,1},{1,0},{1,1},{1,2},{1,7},{1,8},{1,13},{1,14},{1,19},{2,3},{2,6},{2,9},{2,12},{2,15},{2,18}},
  {{2,1},{1,0},{1,1},{1,2},{1,7},{1,8},{1,13},{1,14},{1,19},{0,3},{0,6},{0,9},{0,12},{0,15},{0,18}}
};
pair<int,int> monster_size = {2,19};
vector<string> image;
void find_monster_horizontal(ul r, ul c) {
  for (auto m : monsters_horizontal) {
    bool found = true;
    for (auto p : m) {
      if (image[r+p.first][c+p.second] == '.') {
        found = false;
        break;
      }
    }
    if (found) {
      for (auto p : m) {
        image[r+p.first][c+p.second] = 'O';
      }
    }
  }
}

void find_monster_vertical(ul r, ul c) {
  for (auto m : monsters_horizontal) {
    bool found = true;
    for (auto p : m) {
      if (image[r+p.second][c+p.first] == '.') {
        found = false;
        break;
      }
    }
    if (found) {
      for (auto p : m) {
        image[r+p.second][c+p.first] = 'O';
      }
    }
  }
}

void print_image() {
  for (ul r = 0; r < image.size(); ++r) {
    debug() << image[r];
  }
}

int main() {
  string line;
  vector<string> tile;
  ul last;
  while (getline(cin, line)) {
    if (line.length() == 0) {
      tiles.insert({last, vector<string>(tile)});
      tile.clear();
      continue;
    }
    if (line[0] == 'T') {
      last = stol(line.substr(5, 4));
      continue;
    }
    tile.push_back(line);
  }
  tiles.insert({last, vector<string>(tile)});

  for (auto t1 : tiles) {
    for (auto t2 : tiles) {
      if (t1.first == t2.first) continue;
      save_matches(t1.first, t2.first);
    }
  }

  set<ul> seen;
  for (auto first : right_matches) {
    if (seen.find(first.first) != seen.end()) continue;
    auto new_rows = rowify(first.first);
    rows.insert(rows.end(), new_rows.begin(), new_rows.end());
  }

  vector<row> solution;
  for (auto r : rows) {
    if (r[0] != 278900 || r[TILING_SIZE-1] != 332911) continue;
    solution = loop(add_seen({}, r), r, {r});
    if (solution.size() == 0) continue;
    break;
  }

  debug() << solution[0][0];

  for (auto r : solution) {
    vector<vector<string>> os;
    for (auto t : r)
      os.push_back(orientate(t));
    for (ul i = 1; i < TILE_SIZE; ++i) {
      string l;
      for (auto t : os) {
        l += t[i].substr(1,TILE_SIZE-1);
      }
      image.push_back(l);
    }
  }

  print_image();


  for (ul r = 0; r < image.size()-monster_size.first; ++r) {
    for (ul c = 0; c < image[r].size()-monster_size.second; ++c) {
      find_monster_horizontal(r,c);
    }
  }
  for (ul r = 0; r < image.size()-monster_size.second; ++r) {
    for (ul c = 0; c < image[r].size()-monster_size.first; ++c) {
      find_monster_vertical(r,c);
    }
  }

  print_image();

  ul count = 0;
  for (ul r = 0; r < image.size(); ++r) {
    for (ul c = 0; c < image[r].size(); ++c) {
      if (image[r][c] == '#') ++count;
    }
  }
  cout << count << endl;
  
  return 0;
}

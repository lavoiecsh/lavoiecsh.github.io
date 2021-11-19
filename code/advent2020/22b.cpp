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

typedef deque<uint> deck;

deck take(uint count, deck *d) {
  deck n;
  for (uint i = 0; i < count; ++i)
    n.push_back(d->at(i));
  return n;
}

bool play_game(deck *player1, deck *player2);
void play_round(deck *player1, deck *player2) {
  debug() << "round" pp(*player1) pp(*player2);
  uint p1 = player1->front();
  player1->pop_front();
  uint p2 = player2->front();
  player2->pop_front();

  bool winner = p1 > p2;
  if (p1 <= player1->size() && p2 <= player2->size()) {
    deck player1b = take(p1, player1);
    deck player2b = take(p2, player2);
    winner = play_game(&player1b, &player2b);
  }

  if (winner) {
    player1->push_back(p1);
    player1->push_back(p2);
  } else {
    player2->push_back(p2);
    player2->push_back(p1);
  }
}

string stringify(deck *player1, deck *player2) {
  stringstream ss;
  for (uint i = 0; i < player1->size(); ++i)
    ss << player1->at(i) << ",";
  ss << ";";
  for (uint i = 0; i < player2->size(); ++i)
    ss << player2->at(i) << ",";
  return ss.str();
}

bool play_game(deck *player1, deck *player2) {
  debug() << "game" pp(*player1) pp(*player2);
  set<string> seen;
  while (!player1->empty() && !player2->empty()) {
    string s = stringify(player1, player2);
    if (seen.find(s) != seen.end()) return true;
    seen.insert(s);
    play_round(player1, player2);
  }
  return player2->empty();
}

int score(deck d) {
  int s = 0;
  ul size = d.size();
  for (ul i = 0; i != size; ++i) {
    s += d[i] * (size - i);
  }
  return s;
}

int main() {
  string line;
  bool reading_first_player = true;
  deck player1, player2;
  while (getline(cin, line)) {
    if (line == "Player 1:" || line == "") {
      continue;
    }
    if (line == "Player 2:") {
      reading_first_player = false;
      continue;
    }

    if (reading_first_player) {
      player1.push_back(stoi(line));
    } else {
      player2.push_back(stoi(line));
    }
  }

  debug() << pp(player1) pp(player2);

  bool player1_winner = play_game(&player1, &player2);
  int s = player1_winner ? score(player1) : score(player2);
  cout << s << endl;
  return 0;
}

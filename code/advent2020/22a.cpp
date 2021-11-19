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

deque<int> player1, player2;

void play_round() {
  int p1 = player1.front();
  player1.pop_front();
  int p2 = player2.front();
  player2.pop_front();

  if (p1 > p2) {
    player1.push_back(p1);
    player1.push_back(p2);
  } else {
    player2.push_back(p2);
    player2.push_back(p1);
  }
}

bool play_game() {
  while (!player1.empty() && !player2.empty()) {
    play_round();
  }
  return player2.empty();
}

int score(deque<int> deck) {
  int s = 0;
  ul size = deck.size();
  for (ul i = 0; i != size; ++i) {
    s += deck[i] * (size - i);
  }
  return s;
}

int main() {
  string line;
  bool reading_first_player = true;
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

  bool player1_winner = play_game();
  int s = player1_winner ? score(player1) : score(player2);
  cout << s << endl;
  return 0;
}

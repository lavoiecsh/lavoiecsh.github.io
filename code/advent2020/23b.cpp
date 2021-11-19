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

const list<int> INPUT = {3,6,2,9,8,1,7,5,4};
const list<int> INPUT_1 = {3,8,9,1,2,5,4,6,7};
const int MAX = 1000000;
const int ITERATIONS = 10000000;

void exec_move(list<int> *cups) {
  int v = cups->front();
  auto it = cups->begin();
  it++;
  vector<int> removed;
  while (removed.size() != 3) {
    removed.push_back(*it);
    it = cups->erase(it);
  }
  int d = v-1;
  if (d == 0) d = MAX;
  while (d == removed[0] || d == removed[1] || d == removed[2]) {
    d--;
    if (d == 0) d = MAX;
  }
  debug() << pp(v) pp(d) pp(removed);
  auto di = find(cups->begin(), cups->end(), d);
  di++;
  debug() << pp(distance(cups->begin(), di));
  cups->insert(di, removed.begin(), removed.end());
  cups->pop_front();
  cups->push_back(v);
}

class Node {
public:
  int value;
  Node *next;
  Node(int v) {
    this->value = v;
    this->next = NULL;
  }
  Node(int v, Node *p) {
    this->value = v;
    this->next = NULL;
    p->next = this;
  }
  void linkTo(Node *n) {
    this->next = n;
  }
};

map<int,Node*> nodes;
Node* exec_move2(Node *current) {
  int v = current->value;
  Node *r1 = current->next;
  Node *r2 = r1->next;
  Node *r3 = r2->next;
  current->linkTo(r3->next);
  int d = v-1;
  if (d == 0) d = MAX;
  while (d == r1->value || d == r2->value || d == r3->value) {
    d--;
    if (d == 0) d = MAX;
  }
  debug() << pp(v) pp(r1->value) pp(r2->value) pp(r3->value) pp(d);
  Node *dn = nodes[d];
  r3->linkTo(dn->next);
  dn->linkTo(r1);
  return current->next;
}

string print(Node *current) {
  Node *z = current;
  stringstream ss;
  do {
    ss << z->value << ",";
    z = z->next;
  } while (z != current);
  return ss.str();
}

void main_node(list<int> cups) {
  Node *start = new Node(cups.front());
  nodes.insert({start->value, start});
  Node *current = start;
  for (auto it = cups.begin(); it != cups.end(); ++it) {
    if (it == cups.begin()) continue;
    Node *next = new Node(*it, current);
    nodes.insert({next->value, next});
    current = next;
  }
  for (int i = 10; i <= MAX; ++i) {
    Node *next = new Node(i, current);
    nodes.insert({next->value, next});
    current = next;
  }
  current->linkTo(start);
  current = current->next;
  // debug() << print(current);
  for (int i = 0; i < ITERATIONS; ++i) {
    current = exec_move2(current);
    // debug() << print(current);
  }
  current = nodes[1];
  cout << current->value << endl;
  cout << current->next->value << endl;
  cout << current->next->next->value << endl;
  cout << (ul)(current->next->value) * (ul)(current->next->next->value) << endl;

  for (auto n : nodes)
    delete(n.second);
}

void main_list(list<int> cups) {
  for (int i = 10; i <= MAX; ++i)
    cups.push_back(i);
  debug() << pp(cups);
  for (int i = 0; i < ITERATIONS; ++i) {
    exec_move(&cups);
    debug() << pp(cups);
  }
  auto i1 = find(cups.begin(), cups.end(), 1);
  if (i1 == cups.end()) i1 = cups.begin();
  auto i2 = i1;
  i2++;
  if (i2 == cups.end()) i2 = cups.begin();
  auto i3 = i2;
  i3++;
  if (i3 == cups.end()) i3 = cups.begin();
  cout << *i1 << endl;
  cout << *i2 << endl;
  cout << *i3 << endl;
  cout << (ul)(*i2) * (ul)(*i3) << endl;
}

int main() {
  main_node(INPUT);
  return 0;
}

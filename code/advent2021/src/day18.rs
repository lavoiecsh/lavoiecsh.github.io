#[allow(unused_macros)] #[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[allow(unused_macros)] #[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

const FILENAME: &str = "inputs/day18.txt";

#[allow(dead_code)]
fn read_input() -> Vec<SnailfishNumber> {
    fs::read_to_string(FILENAME).expect("error reading")
        .trim()
        .lines()
        .map(|l| LineParser::new(l.to_string()).build_snailfish())
        .collect()
}

#[allow(dead_code)]
pub fn part1() -> usize {
    // test("[[[[[9,8],1],2],3],4]");
    // test("[7,[6,[5,[4,[3,2]]]]]");
    // test("[[6,[5,[4,[3,2]]]],1]");
    // test("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");
    
    let numbers = read_input();
    let sum = sum_all(&numbers);
    sum.magnitude()
    // 0
}

// fn test(input: &str) {
//     let mut tmp = LineParser::new(input.to_string()).build_snailfish();
//     debug!(tmp.stringify());
//     tmp.reduce();
//     debug!(tmp.stringify());
// }

#[allow(dead_code)]
pub fn part2() -> usize {
    let numbers = read_input();
    let mut best = 0;
    for a in 0..numbers.len() {
        for b in 0..numbers.len() {
            if a == b {
                continue;
            }
            let mut number = SnailfishNumber::from(&numbers[a]);
            number.add(&numbers[b]);
            let mag = number.magnitude();
            if mag > best {
                best = mag;
            }
        }
    }
    best
}

fn sum_all(numbers: &Vec<SnailfishNumber>) -> SnailfishNumber {
    let mut sum = SnailfishNumber::from(&numbers[0]);
    for n in 1..numbers.len() {
        debug!(sum.stringify());
        sum.add(&numbers[n]);
    }
    debug!(sum.stringify());
    sum
}

type NodeId = usize;

struct Node {
    value: Option<u8>,
    left: Option<NodeId>,
    right: Option<NodeId>,
    parent: Option<NodeId>,
}

impl Node {
    fn copy(&self, offset: NodeId) -> Node {
        Node {
            value: self.value,
            left: match self.left {
                Some(lid) => Some(lid + offset),
                None => None,
            },
            right: match self.right {
                Some(rid) => Some(rid + offset),
                None => None,
            },
            parent: match self.parent {
                Some(pid) => Some(pid + offset),
                None => None,
            }
        }
    }
}

struct SnailfishNumber {
    nodes: Vec<Node>,
    root: NodeId,
}

impl SnailfishNumber {
    fn new() -> SnailfishNumber {
        SnailfishNumber {
            nodes: Vec::new(),
            root: usize::MAX,
        }
    }

    fn from(number: &SnailfishNumber) -> SnailfishNumber {
        SnailfishNumber {
            nodes: number.nodes.iter().map(|n| n.copy(0)).collect(),
            root: number.root,
        }
    }
    
    fn push_value(&mut self, value: u8) -> NodeId {
        let next = self.nodes.len();
        self.nodes.push(Node {
            value: Some(value),
            left: None,
            right: None,
            parent: None
        });
        next
    }

    fn push_pair(&mut self, left: NodeId, right: NodeId) -> NodeId {
        let next = self.nodes.len();
        self.nodes.push(Node {
            value: None,
            left: Some(left),
            right: Some(right),
            parent: None
        });
        self.nodes[left].parent = Some(next);
        self.nodes[right].parent = Some(next);
        next
    }

    fn value(&self, nid: NodeId) -> u8 {
        self.nodes[nid].value.unwrap()
    }

    fn is_value(&self, nid: NodeId) -> bool {
        self.nodes[nid].value.is_some()
    }

    fn left(&self, nid: NodeId) -> NodeId {
        self.nodes[nid].left.unwrap()
    }

    fn right(&self, nid: NodeId) -> NodeId {
        self.nodes[nid].right.unwrap()
    }

    fn parent(&self, nid: NodeId) -> NodeId {
        self.nodes[nid].parent.unwrap()
    }

    fn stringify(&self) -> String {
        self.stringify_node(self.root)
    }

    fn stringify_node(&self, nid: NodeId) -> String {
        if self.is_value(nid) {
            self.value(nid).to_string()
        } else {
            let left = self.left(nid);
            let right = self.right(nid);
            format!("[{},{}]", self.stringify_node(left), self.stringify_node(right))
        }
    }

    fn stringify_with_ids(&self) -> String {
        self.stringify_with_ids_node(self.root)
    }

    fn stringify_with_ids_node(&self, nid: NodeId) -> String {
        if self.is_value(nid) {
            format!("{}/{:?}: {}", nid, self.nodes[nid].parent, self.value(nid))
        } else {
            let left = self.left(nid);
            let right = self.right(nid);
            format!("{}/{:?}: [{}, {}]", nid, self.nodes[nid].parent, self.stringify_with_ids_node(left), self.stringify_with_ids_node(right))
        }
    }

    fn add(&mut self, other: &SnailfishNumber) {
        let offset = self.nodes.len();
        debug!(("starting add", self.root, other.root, offset, self.stringify_with_ids(), other.stringify_with_ids()));
        self.nodes.extend(other.nodes.iter().map(|n| n.copy(offset)));
        let new_root = Node {
            value: None,
            left: Some(self.root),
            right: Some(other.root + offset),
            parent: None,
        };
        let new_rid = self.nodes.len();
        self.nodes.push(new_root);
        self.nodes[self.root].parent = Some(new_rid);
        self.nodes[other.root + offset].parent = Some(new_rid);
        self.root = new_rid;
        debug!(("before reduce", self.stringify_with_ids()));
        self.reduce();
        debug!(("after reduce", self.stringify_with_ids()));
    }

    fn magnitude(&self) -> usize {
        self.magnitude_node(self.root)
    }

    fn magnitude_node(&self, nid: NodeId) -> usize {
        if self.is_value(nid) {
            self.value(nid) as usize
        } else {
            self.magnitude_node(self.left(nid)) * 3 + self.magnitude_node(self.right(nid)) * 2
        }
    }

    fn reduce(&mut self) {
        // debug!("starting reduce");
        let mut modified = true;
        while modified {
            // debug!(self.stringify());
            modified = false;
            match self.n_depth_pair(self.root, 4) {
                Some(to_explode) => {
                    // debug!(("exploding", to_explode, self.value(self.left(to_explode)), self.value(self.right(to_explode))));
                    self.explode(to_explode);
                    modified = true;
                    continue;
                },
                None => {},
            }

            match self.large_number(self.root) {
                Some(to_split) => {
                    // debug!(("splitting", to_split, self.value(to_split)));
                    self.split(to_split);
                    modified = true;
                },
                None => {},
            }
        }
    }

    fn n_depth_pair(&self, nid: NodeId, depth: usize) -> Option<NodeId> {
        if self.is_value(nid) {
            return None
        }

        if depth == 0 {
            return Some(nid)
        }

        self.n_depth_pair(self.left(nid), depth - 1).or(self.n_depth_pair(self.right(nid), depth - 1))
    }

    fn explode(&mut self, nid: NodeId) {
        let lv = self.value(self.left(nid));
        let rv = self.value(self.right(nid));
        self.nodes[nid] = Node {
            value: Some(0),
            left: None,
            right: None,
            parent: self.nodes[nid].parent,
        };
        match self.immediate_left(nid) {
            Some(lid) => {
                self.nodes[lid].value = Some(self.value(lid) + lv);
            },
            None => {
            }
        }
        match self.immediate_right(nid) {
            Some(rid) => {
                self.nodes[rid].value = Some(self.value(rid) + rv);
            },
            None => {
            }
        }
    }

    fn immediate_left(&self, nid: NodeId) -> Option<NodeId> {
        // debug!((self.root, nid, self.nodes[nid].parent));
        let mut tid = nid;
        let mut pid = self.parent(tid);
        // debug!((tid, pid));
        while pid != self.root && tid == self.left(pid) {
            tid = pid;
            pid = self.parent(pid);
            // debug!((tid, pid));
        }
        if pid == self.root && tid == self.left(pid) {
            return None
        }
        tid = self.left(pid);
        while !self.is_value(tid) {
            tid = self.right(tid);
        }
        Some(tid)
    }

    fn immediate_right(&self, nid: NodeId) -> Option<NodeId> {
        let mut tid = nid;
        let mut pid = self.parent(tid);
        while pid != self.root && tid == self.right(pid) {
            tid = pid;
            pid = self.parent(pid);
        }
        if pid == self.root && tid == self.right(pid) {
            return None
        }
        tid = self.right(pid);
        while !self.is_value(tid) {
            tid = self.left(tid);
        }
        Some(tid)
    }

    fn large_number(&self, nid: NodeId) -> Option<NodeId> {
        if self.is_value(nid) {
            return if self.value(nid) >= 10 { Some(nid) } else { None }
        }

        self.large_number(self.left(nid)).or(self.large_number(self.right(nid)))
    }

    fn split(&mut self, nid: NodeId) {
        let value = self.value(nid);
        let lv = value / 2;
        let rv = value - lv;
        let lid = self.push_value(lv);
        self.nodes[lid].parent = Some(nid);
        let rid = self.push_value(rv);
        self.nodes[rid].parent = Some(nid);
        self.nodes[nid] = Node {
            value: None,
            left: Some(lid),
            right: Some(rid),
            parent: self.nodes[nid].parent,
        };
        debug!(("split", nid, value, lv, rv, lid, rid, self.nodes[nid].parent));
    }
}

struct LineParser {
    chars: Vec<char>,
    cursor: usize,
}

impl LineParser {
    fn new(line: String) -> LineParser {
        LineParser {
            chars: line.chars().collect(),
            cursor: 0,
        }
    }

    fn build_snailfish(&mut self) -> SnailfishNumber {
        let mut number = SnailfishNumber::new();
        number.root = self.sub(&mut number);
        number
    }

    fn sub(&mut self, number: &mut SnailfishNumber) -> NodeId {
        if self.chars[self.cursor] != '[' {
            let value = self.chars[self.cursor] as u8 - 48;
            self.cursor += 1;
            return number.push_value(value)
        }

        self.cursor += 1;
        let left = self.sub(number);
        assert!(self.chars[self.cursor] == ',');
        self.cursor += 1;
        let right = self.sub(number);
        assert!(self.chars[self.cursor] == ']');
        self.cursor += 1;
        return number.push_pair(left, right)
    }
}

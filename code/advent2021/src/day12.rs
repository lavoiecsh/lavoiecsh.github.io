#[allow(unused_macros)] #[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[allow(unused_macros)] #[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

const FILENAME: &str = "inputs/day12.txt";

type CaveIndex = usize;

struct Cave {
    name: String,
    adjacent: Vec<CaveIndex>,
}

impl Cave {
    fn is_start(&self) -> bool {
        self.name == "start"
    }

    fn is_end(&self) -> bool {
        self.name == "end"
    }

    fn is_small(&self) -> bool {
        self.name == self.name.to_ascii_lowercase()
    }

    fn is_big(&self) -> bool {
        self.name == self.name.to_ascii_uppercase()
    }
}

struct CaveGraph {
    caves: Vec<Cave>,
}

impl CaveGraph {
    fn new() -> CaveGraph {
        CaveGraph { caves: Vec::new() }
    }
    
    fn add_cave(&mut self, name: &String) {
        if !self.caves.iter().any(|c|c.name == *name) {
            self.caves.push(Cave { name: name.to_string(), adjacent: Vec::new() });
        }
    }

    fn connect(&mut self, cave1: &String, cave2: &String) {
        let cave1_index = self.caves.iter().position(|c|c.name == *cave1).unwrap();
        let cave2_index = self.caves.iter().position(|c|c.name == *cave2).unwrap();
        self.caves[cave1_index].adjacent.push(cave2_index);
        self.caves[cave2_index].adjacent.push(cave1_index);
    }

    fn start(&self) -> CaveIndex {
        self.caves.iter().position(|c|c.name == "start").unwrap()
    }
}

type CanVisitFn = fn (&CaveGraph, CaveIndex, &Vec<CaveIndex>) -> bool;

#[allow(dead_code)]
fn read_input() -> CaveGraph {
    let mut caves: CaveGraph = CaveGraph::new();
    let input = fs::read_to_string(FILENAME).expect("error reading").trim().to_string();
    for connection in input.lines() {
        let mut split = connection.split("-");
        let start = split.next().unwrap().to_string();
        let end = split.next().unwrap().to_string();
        caves.add_cave(&start);
        caves.add_cave(&end);
        caves.connect(&start, &end);
    }
    caves
}

#[allow(dead_code)]
pub fn part1() -> usize {
    let caves = read_input();
    count_paths(&caves, can_visit_1)
}

#[allow(dead_code)]
pub fn part2() -> usize {
    let caves = read_input();
    count_paths(&caves, can_visit_2)
}

fn count_paths(graph: &CaveGraph, can_visit: CanVisitFn) -> usize {
    let mut paths: Vec<Vec<CaveIndex>> = Vec::new();
    paths.push(vec![graph.start()]);
    let mut count = 0;
    while !paths.is_empty() {
        let current = paths.pop().unwrap();
        for path in compute_next(graph, can_visit, &current) {
            if graph.caves[*path.last().unwrap()].is_end() {
                count += 1;
            } else {
                paths.push(path);
            }
        }
    }
    count
}

fn compute_next(graph: &CaveGraph, can_visit: CanVisitFn, visited: &Vec<CaveIndex>) -> Vec<Vec<CaveIndex>> {
    let last = *visited.last().unwrap();
    let mut next_paths = Vec::new();
    for adjacent in &graph.caves[last].adjacent {
        if !can_visit(&graph, *adjacent, &visited) {
            continue;
        }
        let mut next_path = visited.to_vec();
        next_path.push(*adjacent);
        next_paths.push(next_path);
    }
    next_paths
}

fn can_visit_1(graph: &CaveGraph, cave: CaveIndex, visited: &Vec<CaveIndex>) -> bool {
    graph.caves[cave].is_big() ||
        visited.iter().all(|v|*v != cave)
}

fn can_visit_2(graph: &CaveGraph, cave_index: CaveIndex, visited: &Vec<CaveIndex>) -> bool {
    let cave = &graph.caves[cave_index];
    if cave.is_big() {
        return true;
    }
    if cave.is_start() {
        return false;
    }
    if !visited.contains(&cave_index) {
        return true;
    }
    let mut visited_small: Vec<CaveIndex> = visited.iter().filter(|v|graph.caves[**v].is_small()).cloned().collect();
    visited_small.sort();
    for i in 1..visited_small.len() {
        if visited_small[i-1] == visited_small[i] {
            return false;
        }
    }
    true
}

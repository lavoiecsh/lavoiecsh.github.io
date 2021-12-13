#[allow(unused_macros)] #[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[allow(unused_macros)] #[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

use std::collections::HashMap;

const FILENAME: &str = "inputs/day12.txt";

type Caves = HashMap<String, Vec<String>>;
type CanVisitFn = fn (&String, &Vec<String>) -> bool;

#[allow(dead_code)]
fn read_input() -> Caves {
    let mut caves: Caves = HashMap::new();
    for connection in fs::read_to_string(FILENAME).expect("error reading").trim().lines() {
        let mut split = connection.split("-");
        let start = split.next().unwrap();
        let end = split.next().unwrap();
        match caves.get_mut(&start.to_string()) {
            Some(cave) => {
                cave.push(end.to_string());
            },
            None => {
                caves.insert(start.to_string(), vec![end.to_string()]);
            }
        }
        match caves.get_mut(&end.to_string()) {
            Some(cave) => {
                cave.push(start.to_string());
            },
            None => {
                caves.insert(end.to_string(), vec![start.to_string()]);
            }
        }
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

fn count_paths(caves: &Caves, can_visit: CanVisitFn) -> usize {
    let mut paths: Vec<Vec<String>> = Vec::new();
    paths.push(vec!["start".to_string()]);
    let mut count = 0;
    while !paths.is_empty() {
        let current = paths.pop().unwrap();
        for path in compute_next(caves, can_visit, &current) {
            if path.last().unwrap() == "end" {
                count += 1;
            } else {
                paths.push(path);
            }
        }
    }
    count
}

fn compute_next(caves: &Caves, can_visit: CanVisitFn, visited: &Vec<String>) -> Vec<Vec<String>> {
    let last = visited.last().unwrap();
    let mut next_paths = Vec::new();
    for adjacent in caves.get(last).unwrap().iter() {
        if !can_visit(adjacent, visited) {
            continue;
        }
        let mut next_path = visited.to_vec();
        next_path.push(adjacent.to_string());
        next_paths.push(next_path);
    }
    next_paths
}

fn can_visit_1(cave: &String, visited: &Vec<String>) -> bool {
    cave.to_string() == cave.to_ascii_uppercase() ||
        !visited.contains(cave)
}

fn can_visit_2(cave: &String, visited: &Vec<String>) -> bool {
    if cave.to_string() == cave.to_ascii_uppercase() {
        return true;
    }
    if cave.to_string() == "start" {
        return false;
    }
    let counts = count(visited);
    debug!((visited, &counts));
    let max = counts.values().max().unwrap();
    match counts.get(cave) {
        Some(count) => *count == 1 && *max != 2,
        None => true
    }
}

fn count(visited: &Vec<String>) -> HashMap<String, usize> {
    let mut counts = HashMap::new();
    for v in visited {
        if v.to_string() == v.to_ascii_uppercase() {
            continue;
        }
        counts.insert(v.to_string(), counts.get(v).unwrap_or(&0) + 1);
    }
    counts
}

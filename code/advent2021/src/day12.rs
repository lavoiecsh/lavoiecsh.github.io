#[allow(unused_macros)] #[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[allow(unused_macros)] #[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

use std::collections::HashMap;

const FILENAME: &str = "inputs/day12.txt";

struct Cave {
    adjacent: Vec<String>,
}

#[allow(dead_code)]
fn read_input() -> HashMap<String, Cave> {
    let mut caves: HashMap<String, Cave> = HashMap::new();
    for connection in fs::read_to_string(FILENAME).expect("error reading").trim().lines() {
        let mut split = connection.split("-");
        let start = split.next().unwrap();
        let end = split.next().unwrap();
        match caves.get_mut(&start.to_string()) {
            Some(cave) => {
                cave.adjacent.push(end.to_string());
            },
            None => {
                caves.insert(start.to_string(), Cave { adjacent: vec![end.to_string()] });
            }
        }
        match caves.get_mut(&end.to_string()) {
            Some(cave) => {
                cave.adjacent.push(start.to_string());
            },
            None => {
                caves.insert(end.to_string(), Cave { adjacent: vec![start.to_string()] });
            }
        }
    }
    caves
}

#[allow(dead_code)]
pub fn part1() -> usize {
    let caves = read_input();
    let paths = generate_paths(&caves, can_visit_1);
    paths.len()
}

#[allow(dead_code)]
pub fn part2() -> usize {
    let caves = read_input();
    let paths = generate_paths(&caves, can_visit_2);
    debug!(&paths);
    paths.len()
}

fn generate_paths(caves: &HashMap<String, Cave>, can_visit: fn(&String, &Vec<String>) -> bool) -> Vec<Vec<String>> {
    let mut paths: Vec<Vec<String>> = Vec::new();
    paths.push(vec!["start".to_string()]);
    let mut prev_len = 0;
    while paths.len() != prev_len {
        prev_len = paths.len();
        let mut next_paths: Vec<Vec<String>> = Vec::new();
        for path in paths {
            if path.last().unwrap().to_string() == "end" {
                next_paths.push(path.to_vec());
            } else {
                next_paths.extend(compute_next(caves, can_visit, &path));
            }
        }
        paths = next_paths;
    }
    paths
}

fn compute_next(caves: &HashMap<String, Cave>, can_visit: fn(&String, &Vec<String>) -> bool, visited: &Vec<String>) -> Vec<Vec<String>> {
    let last = visited.last().unwrap();
    let mut next_paths = Vec::new();
    for adjacent in caves.get(last).unwrap().adjacent.iter() {
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

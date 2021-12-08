#[allow(unused_macros)] #[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[allow(unused_macros)] #[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

const FILENAME: &str = "inputs/day08.txt";

const SEGMENT_COUNT: [usize; 10] = [6, 2, 5, 5, 4, 5, 6, 3, 7, 6];

struct Entry {
    patterns: Vec<String>,
    outputs: Vec<String>,
}

#[allow(dead_code)]
fn read_input() -> Vec<Entry> {
    fs::read_to_string(FILENAME).expect("error reading")
        .trim()
        .lines()
        .map(line_to_entry)
        .collect()
}

#[allow(dead_code)]
pub fn part1() -> usize {
    let entries = read_input();
    entries.iter()
        .flat_map(|entry| entry.outputs.to_vec())
        .fold(0, |acc, output| acc + if is_1478(&output) { 1 } else { 0 })
}

#[allow(dead_code)]
pub fn part2() -> usize {
    let entries = read_input();
    entries.iter()
        .fold(0, |acc, entry| acc + calculate(&entry))
}

fn line_to_entry(line: &str) -> Entry {
    let mut sides = line.split(" | ");
    Entry {
        patterns: sides.next().unwrap().split(" ").map(String::from).map(sort_string).collect(),
        outputs: sides.next().unwrap().split(" ").map(String::from).map(sort_string).collect(),
    }
}

fn sort_string(input: String) -> String {
    let mut chars: Vec<char> = input.chars().collect();
    chars.sort();
    String::from_iter(chars.iter())
}

fn is_1478(item: &String) -> bool {
    item.len() == SEGMENT_COUNT[1] ||
        item.len() == SEGMENT_COUNT[4] ||
        item.len() == SEGMENT_COUNT[7] ||
        item.len() == SEGMENT_COUNT[8]
}

fn calculate(entry: &Entry) -> usize {
    const STRING: String = String::new();
    let mut found_patterns: [String; 10] = [STRING; 10];
    found_patterns[1] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[1]).unwrap().to_string();
    found_patterns[4] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[4]).unwrap().to_string();
    found_patterns[7] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[7]).unwrap().to_string();
    found_patterns[8] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[8]).unwrap().to_string();

    found_patterns[9] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[9] && contains_all(&found_patterns[4], p)).unwrap().to_string();
    found_patterns[0] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[0] && **p != found_patterns[9] && contains_all(&found_patterns[7], p)).unwrap().to_string();
    found_patterns[6] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[6] && **p != found_patterns[0] && **p != found_patterns[9]).unwrap().to_string();

    found_patterns[3] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[3] && contains_all(&found_patterns[1], p)).unwrap().to_string();
    found_patterns[5] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[5] && **p != found_patterns[3] && contains_all(p, &found_patterns[9])).unwrap().to_string();
    found_patterns[2] = entry.patterns.iter().find(|p| p.len() == SEGMENT_COUNT[2] && **p != found_patterns[3] && **p != found_patterns[5]).unwrap().to_string();

    debug!(&found_patterns);

    debug!(entry.outputs.iter().fold(0, |acc, o| acc*10 + lookup(&found_patterns, o)))
}

fn contains_all(search: &String, input: &String) -> bool {
    search.chars()
        .fold(true, |acc, c| acc && input.contains(c))
}

fn lookup(patterns: &[String; 10], output: &String) -> usize {
    
    for n in 0..10 {
        if patterns[n] == *output {
            return n;
        }
    }
    usize::MAX
}

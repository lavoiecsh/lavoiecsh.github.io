#[allow(unused_macros)] #[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[allow(unused_macros)] #[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

#[allow(dead_code)]
fn read_input() -> String {
    fs::read_to_string("inputs/day00.txt").expect("error reading")
}

#[allow(dead_code)]
pub fn part1() -> usize {
    0
}

#[allow(dead_code)]
pub fn part2() -> usize {
    0
}

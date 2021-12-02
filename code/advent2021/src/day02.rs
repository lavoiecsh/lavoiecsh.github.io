#[allow(unused_macros)] #[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[allow(unused_macros)] #[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

enum Direction {
    Forward,
    Down,
    Up
}

struct Command {
    direction: Direction,
    units: usize,
}

#[allow(dead_code)]
fn read_input() -> Vec<Command> {
    fs::read_to_string("inputs/day02.txt").expect("error reading")
        .trim()
        .split("\n")
        .map(parse_line)
        .collect()
}

fn parse_line(input: &str) -> Command {
    let mut sections = input.split(" ");
    let dir = sections.next().unwrap();
    let units = sections.next().unwrap().parse().expect("error parsing");
    match dir {
        "forward" => Command { direction: Direction::Forward, units: units },
        "down" => Command { direction: Direction::Down, units: units },
        "up" => Command { direction: Direction::Up, units: units },
        _ => panic!("not found")
    }
}

#[allow(dead_code)]
pub fn part1() -> usize {
    let commands = read_input();
    let mut pos: usize = 0;
    let mut depth: usize = 0;
    for command in commands {
        match command.direction {
            Direction::Forward => pos += command.units,
            Direction::Down => depth += command.units,
            Direction::Up => depth -= command.units,
        }
    }
    pos * depth
}

#[allow(dead_code)]
pub fn part2() -> usize {
    let commands = read_input();
    let mut pos: usize = 0;
    let mut depth: usize = 0;
    let mut aim: usize = 0;
    for command in commands {
        match command.direction {
            Direction::Down => aim += command.units,
            Direction::Up => aim -= command.units,
            Direction:: Forward => {
                pos += command.units;
                depth += aim * command.units;
            }
        }
    }
    pos * depth
}

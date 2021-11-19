use std::fs;

fn read_input() -> Vec<i32> {
    fs::read_to_string("inputs/day01.txt").expect("error reading").trim().split("\n").map(|s| s.parse().expect("error parsing")).collect()
}

#[allow(dead_code)]
pub fn part1() -> i32 {
    read_input().iter().map(fuel_requirement).fold(0, |acc,x| acc + x)
}

#[allow(dead_code)]
pub fn part2() -> i32 {
    read_input().iter().map(fuel_requirement_recursive).fold(0, |acc,x| acc + x)
}

fn fuel_requirement(x: &i32) -> i32 {
    (x / 3) - 2
}

fn fuel_requirement_recursive(x: &i32) -> i32 {
    let y = fuel_requirement(x);
    if y < 0 {
        return 0;
    }
    return y + fuel_requirement_recursive(&y);
}

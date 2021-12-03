#[allow(unused_macros)] #[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[allow(unused_macros)] #[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

const SIZE: u32 = 12;

#[allow(dead_code)]
fn read_input() -> Vec<Vec<char>>{ 
    fs::read_to_string("inputs/day03.txt").expect("error reading")
        .trim()
        .lines()
        .map(|s|s.chars().collect())
        .collect()
}

#[allow(dead_code)]
pub fn part1() -> usize {
    let numbers = read_input();
    let mut power: usize = usize::pow(2, SIZE-1);
    let mut gamma: usize = 0;
    let mut epsilon: usize = 0;
    for i in 0..SIZE as usize {
        let mut count0: usize = 0;
        for n in &numbers {
            if n[i] == '0' {
                count0 += 1;
            }
        }
        if count0 > numbers.len()/2 {
            epsilon += power;
        } else {
            gamma += power;
        }
        power /= 2;
    }
    gamma * epsilon
}

#[allow(dead_code)]
pub fn part2() -> usize {
    let numbers = read_input();
    let oxygen: usize = find_oxygen(&numbers);
    let co2: usize = find_co2(&numbers);
    debug!(oxygen);
    debug!(co2);
    oxygen * co2
}

fn find_oxygen(start: &Vec<Vec<char>>) -> usize {
    let mut numbers: Vec<Vec<char>> = start.iter().map(|n|n.to_vec()).collect();
    for i in 0..SIZE as usize {
        if numbers.len() == 1 {
            return unbinary(&numbers[0]);
        }
        let c0 = count0(&numbers, i);
        let c1 = numbers.len() - c0;
        if c0 > c1 {
            numbers = numbers.iter().filter(|n|n[i] == '0').map(|n|n.to_vec()).collect();
        } else {
            numbers = numbers.iter().filter(|n|n[i] == '1').map(|n|n.to_vec()).collect();
        }
    }
    assert!(numbers.len() == 1);
    unbinary(&numbers[0])
}

fn find_co2(start: &Vec<Vec<char>>) -> usize {
    let mut numbers: Vec<Vec<char>> = start.iter().map(|n|n.to_vec()).collect();
    for i in 0..SIZE as usize {
        debug!(numbers.len());
        if numbers.len() == 1 {
            return unbinary(&numbers[0]);
        }
        let c0 = count0(&numbers, i);
        let c1 = numbers.len() - c0;
        debug!(c0);
        debug!(c1);
        if c0 <= c1 {
            numbers = numbers.iter().filter(|n|n[i] == '0').map(|n|n.to_vec()).collect();
        } else {
            numbers = numbers.iter().filter(|n|n[i] == '1').map(|n|n.to_vec()).collect();
        }
    }
    assert!(numbers.len() == 1);
    unbinary(&numbers[0])
}

fn unbinary(n: &Vec<char>) -> usize {
    let mut b: usize = 0;
    let mut power: usize = usize::pow(2, SIZE-1);
    for i in 0..SIZE as usize {
        if n[i] == '1' {
            b += power;
        }
        power /= 2;
    }
    b
}

fn count0(numbers: &Vec<Vec<char>>, i: usize) -> usize {
    numbers.iter().fold(0, |acc,n| if n[i] == '0' { acc + 1 } else { acc })
}

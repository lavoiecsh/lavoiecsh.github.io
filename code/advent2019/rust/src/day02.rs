#[cfg(debug_assertions)] macro_rules! debug { ($x: expr) => { dbg!($x) }}
#[cfg(not(debug_assertions))] macro_rules! debug { ($x:expr) => { std::convert::identity($x) }}

use std::fs;

fn read_input() -> Vec<usize> {
    fs::read_to_string("inputs/day02.txt").expect("error reading").trim().split(",").map(|x| x.parse().expect("error parsing")).collect()
}

#[allow(dead_code)]
pub fn part1() -> usize {
    let mut program = Program {
        ip: 0,
        is_halted: false,
        memory: read_input(),
    };
    program.memory[2] = 2;
    program.memory[1] = 12;
    program.execute();
    program.memory[0]
}

#[allow(dead_code)]
pub fn part2() -> usize {
    let memory = read_input();
    for noun in 0..99 {
        for verb in 0..99 {
            let mut p = Program {
                ip: 0,
                memory: memory.to_vec(),
                is_halted: false,
            };
            p.memory[1] = noun;
            p.memory[2] = verb;
            p.execute();
            if p.memory[0] == 19690720 {
                return noun * 100 + verb;
            }
        }
    }
    return 0;
}

struct Program {
    ip: usize,
    memory: Vec<usize>,
    is_halted: bool,
}

impl Program {
    fn execute(&mut self) {
        debug!(&self.memory);
        while !self.is_halted {
            self.step();
            debug!(&self.memory);
        }
    }

    fn step(&mut self) {
        match self.memory[self.ip] {
            1 => {
                let c = self.memory[self.ip+3];
                self.memory[c] = self.memory[self.memory[self.ip+1]] + self.memory[self.memory[self.ip+2]];
                self.ip += 4;
            }
            2 => {
                let c = self.memory[self.ip+3];
                self.memory[c] = self.memory[self.memory[self.ip+1]] * self.memory[self.memory[self.ip+2]];
                self.ip += 4;
            }
            99 => {
                self.is_halted = true;
            },
            _ => { },
        }
    }
}

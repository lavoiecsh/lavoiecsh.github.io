use std::time::SystemTime;

mod day00;
mod day01;
mod day02;

#[allow(unused_imports)]
use day02::{part1,part2};

fn main() {
    println!("\nExecution Starting\n");
    execute(part1);
    execute(part2);
    println!("\nExecution Completed");
}

fn execute(part: fn() -> usize) {
    let now = SystemTime::now();
    let solution = part();
    let elapsed_result = now.elapsed();
    println!("Solution: {}", solution);
    match elapsed_result {
        Ok(elapsed) => {
            println!("Duration: {}s {:0>3}_{:0>3}_{:0>3}ns",
                     elapsed.as_secs(),
                     elapsed.subsec_millis(),
                     elapsed.subsec_micros() % 1000,
                     elapsed.subsec_nanos() % 1000);
        }
        Err(error) => {
            println!("Duration errored: {:?}", error);
        }
    }
}

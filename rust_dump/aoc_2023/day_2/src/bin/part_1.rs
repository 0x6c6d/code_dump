use regex::Regex;
use std::io::Error;

const RED: u32 = 12;
const GREEN: u32 = 13;
const BLUE: u32 = 14;

fn main() {
    let input = include_str!("./input_1.txt");
    match process(input) {
        Ok(result) => println!("{}", result),
        Err(error) => println!("{}", error),
    }
}

fn process(input: &str) -> Result<u32, Error> {
    let result: u32 = input
        .lines()
        .filter(|&line| game_pass(line))
        .map(|line| extract_game_number(&line))
        .sum();

    Ok(result)
}

fn game_pass(input: &str) -> bool {
    // split line by , ; or : & remove the "Game n:" part
    let split: Vec<&str> = input
        .split(|c| c == ':' || c == ';' || c == ',')
        .skip(1)
        .collect();

    // trim each item
    let trimmed: Vec<&str> = split.iter().map(|s| s.trim()).collect();

    // check if game is possible
    let result: bool = trimmed.iter().all(|&element| {
        if let Some((number_str, color)) = extract_number_and_color(element) {
            if let Ok(number) = number_str.parse::<u32>() {
                if color == "red" {
                    println!("{color}: {number} < {RED}");
                    number <= RED
                } else if color == "blue" {
                    println!("{color}: {number} < {BLUE}");
                    number <= BLUE
                } else if color == "green" {
                    println!("{color}: {number} < {GREEN}");
                    number <= GREEN
                } else {
                    true
                }
            } else {
                true
            }
        } else {
            true
        }
    });

    result
}

fn extract_game_number(input: &str) -> u32 {
    let re = Regex::new(r"Game (\d+):").unwrap();
    if let Some(captures) = re.captures(input) {
        if let Some(number) = captures.get(1) {
            let output = number.as_str().parse().unwrap();
            println!("Extract game number: {input} -> {output}");
            return output;
        }
    }

    0
}

fn extract_number_and_color(s: &str) -> Option<(&str, &str)> {
    let parts: Vec<&str> = s.splitn(2, char::is_whitespace).collect();
    if parts.len() == 2 {
        Some((parts[0], parts[1]))
    } else {
        None
    }
}

#[cfg(test)]
mod test {
    use super::*;

    #[test]
    fn test_code() {
        let input = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

        match process(input) {
            Ok(result) => assert_eq!("8", result.to_string()),
            Err(error) => println!("{error}"),
        }
    }
}

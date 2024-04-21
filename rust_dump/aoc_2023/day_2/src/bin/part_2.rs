use std::io::Error;

fn main() {
    let input = include_str!("./input_2.txt");
    match process(input) {
        Ok(result) => println!("{}", result),
        Err(error) => println!("{}", error),
    }
}

fn process(input: &str) -> Result<u32, Error> {
    let result: u32 = input.lines().map(|line| get_line_power(&line)).sum();
    Ok(result)
}

fn get_line_power(input: &str) -> u32 {
    let mut blue: u32 = 0;
    let mut red: u32 = 0;
    let mut green: u32 = 0;

    let split: Vec<&str> = input
        .split(|c| c == ':' || c == ';' || c == ',')
        .skip(1)
        .collect();
    let trimmed: Vec<&str> = split.iter().map(|s| s.trim()).collect();

    trimmed.iter().all(|&element| {
        if let Some((number_str, color)) = extract_number_and_color(element) {
            if let Ok(number) = number_str.parse::<u32>() {
                if number > blue && color == "blue" {
                    blue = number;
                    println!("Blue {blue}");
                    true
                } else if number > red && color == "red" {
                    red = number;
                    println!("Red {red}");
                    true
                } else if number > green && color == "green" {
                    green = number;
                    println!("Green {green}");
                    true
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

    println!("Blue {blue}, Red {red}, Green {green}");
    blue * red * green
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
            Ok(result) => assert_eq!("2286", result.to_string()),
            Err(error) => println!("{error}"),
        }
    }
}

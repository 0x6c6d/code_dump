use std::io::Error;

fn main() {
    let input = include_str!("./input_2.txt");
    match process(input) {
        Ok(result) => println!("{result}"),
        Err(error) => println!("{error}"),
    }
}

fn process(_input: &str) -> Result<String, Error> {
    let result = _input
        .lines()
        .map(|line| {
            println!("Line: {line}");
            let mut iterator = (0..line.len()).filter_map(|index| {
                let reduced_line = &line[index..];
                println!("Index: {index}, Reduced Line: {reduced_line}");
                let output = if reduced_line.starts_with("one") {
                    '1'
                } else if reduced_line.starts_with("two") {
                    '2'
                } else if reduced_line.starts_with("three") {
                    '3'
                } else if reduced_line.starts_with("four") {
                    '4'
                } else if reduced_line.starts_with("five") {
                    '5'
                } else if reduced_line.starts_with("six") {
                    '6'
                } else if reduced_line.starts_with("seven") {
                    '7'
                } else if reduced_line.starts_with("eight") {
                    '8'
                } else if reduced_line.starts_with("nine") {
                    '9'
                } else {
                    reduced_line.chars().next().unwrap()
                };

                output.to_digit(10)
            });

            let first = iterator.next().expect("Should be a number");
            match iterator.last() {
                Some(number) => format!("{first}{number}"),
                None => format!("{first}{first}"),
            }
            .parse::<u32>()
            .expect("Should be a number")
        })
        .sum::<u32>();

    Ok(result.to_string())
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_code() {
        let input = "two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";
        match process(input) {
            Ok(result) => assert_eq!("281", result),
            Err(error) => println!("{}", error),
        }
    }
}

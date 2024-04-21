fn main() {
    let input = include_str!("./input_1.txt");
    let result = process(input);
    println!("{}", result);
}

fn process(_input: &str) -> u32 {
    // create a vector to store each line & number
    let mut numbers = Vec::new();
    let mut lines = Vec::new();
    for line in _input.lines() {
        lines.push(line);
    }

    let mut first: char = '0';
    let mut second: char = '0';
    for line in lines {
        // get first number in line
        for char in line.chars() {
            if char.is_numeric() {
                first = char;
                break;
            }
        }

        // get second number in line
        for char in line.chars().rev() {
            if char.is_numeric() {
                second = char;
                break;
            }
        }

        let number = format!("{}{}", first, second);
        if let Ok(number) = number.parse::<u32>() {
            numbers.push(number);
        }
    }

    numbers.iter().sum()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_code() {
        let input = "1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";
        let result = process(input);
        assert_eq!("142", result.to_string());
    }
}

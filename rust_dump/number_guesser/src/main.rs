use std::io;
use std::cmp::Ordering;
use rand::Rng;

fn main() {
    println!("Guess a number between 1 and 10");

    loop {
        println!("Enter your guess here:");

        let mut guess = String::new();

        io::stdin()
        .read_line(&mut guess)
        .expect("Failed to read line!");

        let guess: u32 = match guess.trim().parse() {
            Ok(num) => num,
            Err(_) => continue
        };

        println!("You enterd: {guess}");

        let secret_number = rand::thread_rng().gen_range(1..=10);

        println!("The secret number is: {secret_number}");

        match guess.cmp(&secret_number) {
            Ordering::Less => println!("Too small"),
            Ordering::Greater => println!("Too big"),
            Ordering::Equal => {
                println!("Correct!!!!");
                break;
            }
        }
    }
}

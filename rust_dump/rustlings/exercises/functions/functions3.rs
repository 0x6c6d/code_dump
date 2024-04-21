// functions3.rs
//
// Execute `rustlings hint functions3` or use the `hint` watch subcommand for a
// hint.

fn main() {
    let number: u32 = 10;
    call_me(&number);
}

fn call_me(num: &u32) {
    for i in 0..*num {
        println!("Ring! Call number {}", i + 1);
    }
}

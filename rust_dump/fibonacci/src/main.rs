use bpaf::Bpaf;

const MIN: u16 = 0;
const MAX: u16 = 20;

#[derive(Debug, Clone, Bpaf)]
#[bpaf(options)]
pub struct Arguments {
    #[bpaf(long, short, guard(guard_nth_fib, "Number must be between 0 and 20"))]
    /// Calculates the nth number of the fibonacci sequence. Number must be between 0 and 20.
    pub nth_fib: u16
}

fn guard_nth_fib(input: &u16) -> bool {
    MIN <= *input && *input <= MAX
}

fn calc_fib(nth_fib: &u16) -> u16 {
    let mut counter: u16 = *nth_fib;

    match *nth_fib {
        0 => 0,
        1 => 1,
        _ => {
            let mut n1: u16 = 0;
            let mut n2: u16 = 1;
            let mut n3: u16 = n1 + n2;

            while counter > 2 {
                counter -= 1;
                n1 = n2;
                n2 = n3;
                n3 = n1 + n2;
            }

            n3
        }
    }
}

fn main() {
   let opts: Arguments = arguments().run();
   let result = calc_fib(&opts.nth_fib);

    println!("The {}th fibonacci number is {}.", opts.nth_fib, result);
}

fn main() {
    // STACK
    // stack only data gets copied
    let x: u8 = 5;
    let y: u8 = x;
    println!("x: {} {:p}\ny: {} {:p}", x, &x, y, &y);

    // HEAP
    // data on the heap only gets referenced via pointers
    let s1 = String::from("test"); // each heap data only has one owner -> s1
    println!("s1: {} {:p}", &s1, &s1);
    let s2 = s1; // ownership moves from s1 to s2 -> s1 gets dropped
                 // rust doesn't do shallow copies -> avoid dangling pointers
    println!("s2: {} {:p}", s2, &s2); // moving values creates new pointer -> prevent double freeing memory
    let s3 = s2.clone(); // data on heap gets cloned -> s2 & s3 point to different memory addresses
    println!("s3: {} {:p}", s3, &s3);

    // STRUCT
    #[derive(Debug)]
    struct Rectangle {
        width: u32,
        height: u32,
    }
    impl Rectangle {
        fn area(&self) -> u32 {
            self.width * self.height
        }
        fn can_hold(&self, other: &Rectangle) -> bool {
            self.width * self.height >= other.area()
        }
    }
    let rec1 = Rectangle {
        width: 10,
        height: 10,
    };
    let rec2 = Rectangle {
        width: 10,
        height: 12,
    };
    if rec2.can_hold(&rec1) {
        println!("Rec2 can hold Rec1: {} - {}", rec2.area(), rec1.area())
    }

    // ENUM
    enum IpAddr {
        IPv4(u8, u8, u8, u8),
        IPv6(String),
    }
    impl IpAddr {
        fn call(&self) {
            match self {
                IpAddr::IPv4(a, b, c, d) => println!("{}.{}.{}.{}", a, b, c, d),
                IpAddr::IPv6(s) => println!("{}", s),
            }
        }
    }
    let ipv4 = IpAddr::IPv4(127, 0, 0, 1);
    ipv4.call();
}

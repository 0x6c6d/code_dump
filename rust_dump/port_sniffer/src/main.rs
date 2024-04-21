use bpaf::Bpaf;
use std::io::{self, Write};
use std::net::{IpAddr, Ipv4Addr};
use std::sync::mpsc::{channel, Sender};
use tokio::net::TcpStream;
use tokio::task;

// constants
const MAX: u16 = 65535;
const IPFALLBACK: IpAddr = IpAddr::V4(Ipv4Addr::new(127, 0, 0, 1));

// cli arguments
#[derive(Debug, Clone, Bpaf)]
#[bpaf(options)]
pub struct Arguments {
    #[bpaf(long, short, fallback(IPFALLBACK))]
    /// The address that you want to sniff. It must be a valid IPv4 address. The fallback value
    /// will be 127.0.0.1 (localhost).
    pub address: IpAddr,
    #[bpaf(
        long("start"),
        short('s'),
        fallback(1u16),
        guard(start_port_guard, "Must be greater than 0.")
    )]
    /// The start port for the sniffer. It must be greater than 0.
    pub start_port: u16,

    /// The end port for the sniffer. It must be smaller then 65536.
    #[bpaf(
        long("end"),
        short('e'),
        fallback(MAX),
        guard(end_port_guard, "Must be smaller than 65536.")
    )]
    pub end_port: u16,
}

fn start_port_guard(input: &u16) -> bool {
    *input > 0
}

fn end_port_guard(input: &u16) -> bool {
    *input <= MAX
}

// fn that scans the ports of the given ip address
async fn scan(tx: Sender<u16>, port: u16, addr: IpAddr) {
    // attempts to connect to a port
    match TcpStream::connect(format!("{addr}:{port}")).await {
        // on success '.' will be printed
        Ok(_) => {
            print!(".");
            io::stdout().flush().unwrap();
            tx.send(port).unwrap();
        }
        // ignore unused ports
        Err(_) => {}
    }
}

#[tokio::main]
async fn main() {
    // collect the given cli argument
    let opts: Arguments = arguments().run();
    // initialize channel
    let (tx, rx) = channel();
    // create a vector for the output
    let mut ports = vec![];

    // iterate through all of the ports based on the arguments from the cli
    for i in opts.start_port..opts.end_port {
        let tx = tx.clone();

        // use a new thread for each port
        task::spawn(async move { scan(tx, i, opts.address).await });
    }

    // drop all the tx clones
    drop(tx);

    // push all the used ports into to vector
    for p in rx {
        ports.push(p);
    }

    if ports.len() == 0 {
        println!("No open ports under ip {}", opts.address);
        return;
    }

    // sort & output the result
    println!("");
    ports.sort();
    for port in ports {
        println!("{} is open", port);
    }
}

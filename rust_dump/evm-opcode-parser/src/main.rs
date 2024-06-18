mod actions;
mod common;

use actions::{all, cfg, opcodes, split};
use clap::Parser;
use common::cli::ArgsCli;
use common::utils::get_hex_from_file;

use crate::common::cli::Action;

fn main() {
    let args = ArgsCli::parse();

    let hex: Vec<u8>;
    let bin = get_hex_from_file(&args.path);
    match bin {
        Ok(b) => hex = b,
        Err(e) => {
            println!("Error: {e}");
            return;
        }
    }

    match args.action {
        Action::Split => split::split(&hex),
        Action::Opcodes => {
            let hex_code = split::get_runtime_part(&hex);
            opcodes::opcodes(&hex_code)
        }
        Action::CFG => {
            let hex_code = split::get_runtime_part(&hex);
            cfg::cfg(&hex_code)
        }
        Action::All => all::all(&hex),
    }
}

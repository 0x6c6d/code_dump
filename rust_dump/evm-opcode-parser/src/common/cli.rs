use clap::{Parser, Subcommand};
use std::path::PathBuf;

#[derive(Debug, Parser)]
#[clap(author, version, about, long_about = None)]
pub struct ArgsCli {
    /// Location to the .bin file
    #[arg(short, long, required = true, value_name = "FILE")]
    pub path: PathBuf,

    /// Specify the location where the output file should be placed
    #[arg(short, long, required = false, value_name = "FILE")]
    pub output: Option<String>,

    /// Action to do
    #[command(subcommand)]
    pub action: Action,
}

#[derive(Debug, Subcommand)]
pub enum Action {
    /// Separate the binary into metadata and code sections
    Split,

    /// Displays a chronological list of all opcodes
    Opcodes,

    /// Displays a control flow graph
    CFG,

    /// Executes all commands
    All,
}

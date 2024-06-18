use std::fs::{self};
use std::io::{Error, ErrorKind, Result};
use std::path::PathBuf;

use crate::cfg::Block;
use crate::common::consts::*;

// FILE HANDLING
pub fn get_hex_from_file(path: &PathBuf) -> Result<Vec<u8>> {
    let content = fs::read_to_string(path)?;

    if !content.starts_with("6080604052") {
        return Err(Error::new(
            ErrorKind::Other,
            "The input binary does not originate from a Solidity file",
        ));
    }

    let bytes = match hex::decode(content) {
        Ok(bytes) => bytes,
        Err(_) => {
            return Err(Error::new(
                ErrorKind::Other,
                "Failed to convert the file's content into hexadecimal code",
            ))
        }
    };

    Ok(bytes)
}

// OPCODE HANDLING
pub fn oc_print_list(hex: &Vec<u8>) {
    oc_print_lines(hex, &0);
}

pub fn oc_print_blocks(blocks: &Vec<Block>) {
    for block in blocks {
        oc_print_lines(&block.opcodes, &block.offset)
    }
}

fn oc_print_lines(hex: &Vec<u8>, idx: &usize) {
    let mut i = 0;
    let num_digits = (hex.len() as f64).log10().ceil() as usize;

    while i < hex.len() {
        let index = format!("{:0width$}", i + idx, width = num_digits);
        if let Some(op_name) = oc_to_name(hex[i]) {
            print!("\n{}. {op_name}", index);
        } else {
            print!("\n{}. INVALID", index);
        }

        match hex[i] {
            PUSH1..=PUSH32 => {
                let push_bytes = (hex[i] - PUSH1 + 1) as usize;
                if push_bytes > 0 {
                    print!(" 0x");
                }
                for j in 1..=push_bytes {
                    if i + j < hex.len() {
                        print!("{:02x}", hex[i + j]);
                    } else {
                        print!("Not enough bytes to PUSH. This shouldn't be happening.");
                    }
                }
                i += push_bytes + 1;
            }
            _ => i += 1,
        }
    }
    println!();
}

fn oc_to_name(opcode: u8) -> Option<&'static str> {
    match opcode {
        STOP => Some("STOP"),
        ADD => Some("ADD"),
        MUL => Some("MUL"),
        SUB => Some("SUB"),
        DIV => Some("DIV"),
        SDIV => Some("SDIV"),
        MOD => Some("MOD"),
        SMOD => Some("SMOD"),
        ADDMOD => Some("ADDMOD"),
        MULMOD => Some("MULMOD"),
        EXP => Some("EXP"),
        SIGNEXTEND => Some("SIGNEXTEND"),
        LT => Some("LT"),
        GT => Some("GT"),
        SLT => Some("SLT"),
        SGT => Some("SGT"),
        EQ => Some("EQ"),
        ISZERO => Some("ISZERO"),
        AND => Some("AND"),
        OR => Some("OR"),
        XOR => Some("XOR"),
        NOT => Some("NOT"),
        BYTE => Some("BYTE"),
        SHL => Some("SHL"),
        SHR => Some("SHR"),
        SAR => Some("SAR"),
        SHA3 => Some("SHA3"),
        ADDRESS => Some("ADDRESS"),
        BALANCE => Some("BALANCE"),
        ORIGIN => Some("ORIGIN"),
        CALLER => Some("CALLER"),
        CALLVALUE => Some("CALLVALUE"),
        CALLDATALOAD => Some("CALLDATALOAD"),
        CALLDATASIZE => Some("CALLDATASIZE"),
        CALLDATACOPY => Some("CALLDATACOPY"),
        CODESIZE => Some("CODESIZE"),
        CODECOPY => Some("CODECOPY"),
        GASPRICE => Some("GASPRICE"),
        EXTCODESIZE => Some("EXTCODESIZE"),
        EXTCODECOPY => Some("EXTCODECOPY"),
        RETURNDATASIZE => Some("RETURNDATASIZE"),
        RETURNDATACOPY => Some("RETURNDATACOPY"),
        EXTCODEHASH => Some("EXTCODEHASH"),
        CHAINID => Some("CHAINID"),
        SELFBALANCE => Some("SELFBALANCE"),
        BASEFEE => Some("BASEFEE"),
        BLOBHASH => Some("BLOBHASH"),
        BLOBBASEFEE => Some("BLOBBASEFEE"),
        BLOCKHASH => Some("BLOCKHASH"),
        COINBASE => Some("COINBASE"),
        TIMESTAMP => Some("TIMESTAMP"),
        NUMBER => Some("NUMBER"),
        PREVRANDAO => Some("PREVRANDAO"),
        GASLIMIT => Some("GASLIMIT"),
        POP => Some("POP"),
        MLOAD => Some("MLOAD"),
        MSTORE => Some("MSTORE"),
        MSTORE8 => Some("MSTORE8"),
        SLOAD => Some("SLOAD"),
        SSTORE => Some("SSTORE"),
        JUMP => Some("JUMP"),
        JUMPI => Some("JUMPI"),
        PC => Some("PC"),
        MSIZE => Some("MSIZE"),
        GAS => Some("GAS"),
        JUMPDEST => Some("JUMPDEST"),
        TLOAD => Some("TLOAD"),
        TSTORE => Some("TSTORE"),
        MCOPY => Some("MCOPY"),
        PUSH0 => Some("PUSH0"),
        PUSH1 => Some("PUSH1"),
        PUSH2 => Some("PUSH2"),
        PUSH3 => Some("PUSH3"),
        PUSH4 => Some("PUSH4"),
        PUSH5 => Some("PUSH5"),
        PUSH6 => Some("PUSH6"),
        PUSH7 => Some("PUSH7"),
        PUSH8 => Some("PUSH8"),
        PUSH9 => Some("PUSH9"),
        PUSH10 => Some("PUSH10"),
        PUSH11 => Some("PUSH11"),
        PUSH12 => Some("PUSH12"),
        PUSH13 => Some("PUSH13"),
        PUSH14 => Some("PUSH14"),
        PUSH15 => Some("PUSH15"),
        PUSH16 => Some("PUSH16"),
        PUSH17 => Some("PUSH17"),
        PUSH18 => Some("PUSH18"),
        PUSH19 => Some("PUSH19"),
        PUSH20 => Some("PUSH20"),
        PUSH21 => Some("PUSH21"),
        PUSH22 => Some("PUSH22"),
        PUSH23 => Some("PUSH23"),
        PUSH24 => Some("PUSH24"),
        PUSH25 => Some("PUSH25"),
        PUSH26 => Some("PUSH26"),
        PUSH27 => Some("PUSH27"),
        PUSH28 => Some("PUSH28"),
        PUSH29 => Some("PUSH29"),
        PUSH30 => Some("PUSH30"),
        PUSH31 => Some("PUSH31"),
        PUSH32 => Some("PUSH32"),
        DUP1 => Some("DUP1"),
        DUP2 => Some("DUP2"),
        DUP3 => Some("DUP3"),
        DUP4 => Some("DUP4"),
        DUP5 => Some("DUP5"),
        DUP6 => Some("DUP6"),
        DUP7 => Some("DUP7"),
        DUP8 => Some("DUP8"),
        DUP9 => Some("DUP9"),
        DUP10 => Some("DUP10"),
        DUP11 => Some("DUP11"),
        DUP12 => Some("DUP12"),
        DUP13 => Some("DUP13"),
        DUP14 => Some("DUP14"),
        DUP15 => Some("DUP15"),
        DUP16 => Some("DUP16"),
        SWAP1 => Some("SWAP1"),
        SWAP2 => Some("SWAP2"),
        SWAP3 => Some("SWAP3"),
        SWAP4 => Some("SWAP4"),
        SWAP5 => Some("SWAP5"),
        SWAP6 => Some("SWAP6"),
        SWAP7 => Some("SWAP7"),
        SWAP8 => Some("SWAP8"),
        SWAP9 => Some("SWAP9"),
        SWAP10 => Some("SWAP10"),
        SWAP11 => Some("SWAP11"),
        SWAP12 => Some("SWAP12"),
        SWAP13 => Some("SWAP13"),
        SWAP14 => Some("SWAP14"),
        SWAP15 => Some("SWAP15"),
        SWAP16 => Some("SWAP16"),
        LOG0 => Some("LOG0"),
        LOG1 => Some("LOG1"),
        LOG2 => Some("LOG2"),
        LOG3 => Some("LOG3"),
        LOG4 => Some("LOG4"),
        CREATE => Some("CREATE"),
        CALL => Some("CALL"),
        CALLCODE => Some("CALLCODE"),
        RETURN => Some("RETURN"),
        DELEGATECALL => Some("DELEGATECALL"),
        CREATE2 => Some("CREATE2"),
        STATICCALL => Some("STATICCALL"),
        REVERT => Some("REVERT"),
        SELFDESTRUCT => Some("SELFDESTRUCT"),
        _ => None,
    }
}

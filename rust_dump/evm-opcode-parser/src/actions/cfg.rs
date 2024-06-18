use crate::common::{consts::*, utils::oc_print_blocks};

// custom structs / enums
pub struct Block {
    pub offset: usize,
    pub length: usize,
    pub block_type: BlockType,
    pub opcodes: Vec<u8>,
    pub jump_to: usize,
    pub jump_type: JumpType,
}

impl Block {
    fn new() -> Self {
        Block {
            offset: 0,
            length: 0,
            block_type: BlockType::None,
            opcodes: Vec::new(),
            jump_to: 0,
            jump_type: JumpType::None,
        }
    }
}

enum BlockType {
    None,
    Entry,
    Exit,
    FunctionBody,
    Dispatcher,
    DispatcherEnd,
}

enum JumpType {
    None,
    Push,
    Orphan,
}

// functions
pub fn cfg(hex: &Vec<u8>) {
    let blocks = split_into_blocks(hex);
    oc_print_blocks(&blocks);
}

fn split_into_blocks(hex: &Vec<u8>) -> Vec<Block> {
    let mut i: usize = 0;
    let mut block = Block::new();
    let mut blocks: Vec<Block> = Vec::new();

    while i < hex.len() {
        block.opcodes.push(hex[i]);

        if hex[i] >= PUSH1 && hex[i] <= PUSH32 {
            let bytes_to_push = hex[i] as usize - 0x5f;
            for k in 1..=bytes_to_push {
                block.opcodes.push(hex[i + k]);
            }
            i += bytes_to_push + 1;
            continue;
        }

        if hex[i] == JUMP
            || hex[i] == JUMPI
            || hex[i] == STOP
            || hex[i] == REVERT
            || hex[i] == RETURN
            || hex[i] == SELFDESTRUCT
            || !is_oc_valid(&hex[i])
            || double_jumpdest(&hex, i)
        {
            // add length
            block.length = block.opcodes.len();

            // add offset
            if blocks.len() == 0 {
                block.offset = 0;
            } else {
                block.offset = i - block.length + 1;
            }

            blocks.push(block);
            block = Block::new();
        }

        i += 1;
    }

    blocks
}

fn is_oc_valid(hex: &u8) -> bool {
    match hex {
        0x0C
        | 0x0D..=0x0F
        | 0x1E..=0x1F
        | 0x21..=0x2F
        | 0x4B..=0x4F
        | 0xA5..=0xAF
        | 0xB3..=0xBF
        | 0xC0..=0xCF
        | 0xD0..=0xDF
        | 0xE0..=0xEF => false,
        _ => true,
    }
}

// create a new block if a two consecutive JUMPDEST opcodes appear
fn double_jumpdest(hex: &Vec<u8>, i: usize) -> bool {
    if i + 1 >= hex.len() {
        return true;
    }

    hex[i + 1] == JUMPDEST
}

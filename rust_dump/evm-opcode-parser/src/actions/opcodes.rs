use crate::common::utils::oc_print_list;

pub fn opcodes(hex: &Vec<u8>) {
    println!("\n ---- OPCODES (Runtime code) ----");
    oc_print_list(hex);
}

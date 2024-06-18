pub fn split(hex: &Vec<u8>) {
    let (code, metadata) = split_code_metadata(hex);
    let (constructor, runtime) = split_constructor_runtime(&code);

    let md = metadata
        .iter()
        .map(|&x| format!("{:02X}", x))
        .collect::<String>();
    let c = constructor
        .iter()
        .map(|&x| format!("{:02X}", x))
        .collect::<String>();
    let r = runtime
        .iter()
        .map(|&x| format!("{:02X}", x))
        .collect::<String>();

    println!("\n ---- SPLIT ----");
    println!("\nMetadata:\n{}", md);
    println!("\nCode - Constructor:\n{}", c);
    println!("\nCode - Runtime:\n{}", r);
}

pub fn get_runtime_part(hex: &Vec<u8>) -> Vec<u8> {
    let (code, _metadata) = split_code_metadata(hex);
    let (_constructor, runtime) = split_constructor_runtime(&code);
    return runtime;
}

fn split_code_metadata(hex: &Vec<u8>) -> (Vec<u8>, Vec<u8>) {
    // The last two hexadecimal bytes encode metadata length information
    let last_two = &hex[hex.len() - 2..];
    let sum: u16 = last_two.iter().map(|&byte| byte as u16).sum();

    // The last two hexadecimal bytes are not included in the length, so they need to be added separately
    let index = hex.len() - (sum + 2) as usize;
    let (code, metadata) = hex.split_at(index);
    (code.to_vec(), metadata.to_vec())
}

fn split_constructor_runtime(hex: &Vec<u8>) -> (Vec<u8>, Vec<u8>) {
    let mut split_at = 0;
    let mut occurrences_found = 0;
    // 0x60, 0x80, 0x60, 0x40, 0x52
    let sequence: [u8; 5] = [96, 128, 96, 64, 82];

    let mut start_index = 0;
    while let Some(index) = hex[start_index..]
        .windows(sequence.len())
        .position(|window| window == sequence)
    {
        occurrences_found += 1;

        if occurrences_found == 2 {
            split_at = start_index + index;
            break;
        }

        start_index += index + 1;
    }

    let (constructor, runtime) = hex.split_at(split_at);
    (constructor.to_vec(), runtime.to_vec())
}

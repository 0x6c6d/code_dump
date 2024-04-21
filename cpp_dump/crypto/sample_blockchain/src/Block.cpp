#include "Block.h"

using std::hash;

// Constructor
Block::Block(int idx, TransactionData td, size_t ph)
    : index(idx), transaction_data(td), previous_hash(ph)
{
    block_hash = generateHash();
}

// Private functions
size_t Block::generateHash()
{
    // creating string of transaction data
    std::string td_str = std::to_string(transaction_data.amount) + transaction_data.receiver_key + transaction_data.sender_key + std::to_string(transaction_data.timestamp);

    // 2 hashes to combine
    std::hash<std::string> td_hash;   // hashes transaction data string
    std::hash<std::string> prev_hash; // re-hashes previous hash (for combination)

    // combine hashes and get size_t for block hash
    return td_hash(td_str) ^ (prev_hash(std::to_string(previous_hash)) << 1);
}

// Public functions
int Block::getIndex()
{
    return index;
}

size_t Block::getHash()
{
    return block_hash;
}

size_t Block::getPreviousHash()
{
    return previous_hash;
}

bool Block::isHashValid()
{
    return generateHash() == getHash();
}

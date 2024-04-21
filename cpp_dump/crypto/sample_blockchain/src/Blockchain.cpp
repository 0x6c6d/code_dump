#include "Blockchain.h"

using std::hash, std::vector;

// Blockchain class implementation
Blockchain::Blockchain()
{
    Block genesis = createGenesisBlock();
    chain.push_back(genesis);
}

// Private functions
Block Blockchain::createGenesisBlock()
{
    // Get Current Time
    std::time_t current;

    // Setup Initial Transaction Data
    TransactionData td(0, "Genesis", "Genesis", time(&current));

    // Return Genesis Block
    Block genesis(0, td, 0);
    return genesis;
}

// Public functions
std::vector<Block> Blockchain::getChain()
{
    return chain;
}

Block Blockchain::getLatestBlock()
{
    return chain.back();
}

bool Blockchain::isChainValid()
{
    vector<Block>::iterator it;

    for (it = chain.begin(); it != chain.end(); ++it)
    {
        Block current_block = *it;
        if (!current_block.isHashValid())
        {
            return false;
        }

        if (it != chain.begin())
        {
            Block previousBlock = *(it - 1);
            if (current_block.getPreviousHash() != previousBlock.getHash())
            {
                return false;
            }
        }
    }

    return true;
}

void Blockchain::addBlock(TransactionData td)
{
    int index = (int)chain.size();
    std::size_t previousHash = (int)chain.size() > 0 ? getLatestBlock().getHash() : 0;

    Block newBlock(index, td, previousHash);
    chain.push_back(newBlock);
}

void Blockchain::printChain()
{
    std::vector<Block>::iterator it;

    for (it = chain.begin(); it != chain.end(); ++it)
    {
        Block current_block = *it;
        printf("\n\nBlock ===================================");
        printf("\nIndex: %d", current_block.getIndex());
        printf("\nAmount: %f", current_block.transaction_data.amount);
        printf("\nSenderKey: %s", current_block.transaction_data.sender_key.c_str());
        printf("\nReceiverKey: %s", current_block.transaction_data.receiver_key.c_str());
        printf("\nTimestamp: %ld", current_block.transaction_data.timestamp);
        printf("\nHash: %zu", current_block.getHash());
        printf("\nPrevious Hash: %zu", current_block.getPreviousHash());
        printf("\nIs Block Valid?: %d\n", current_block.isHashValid());
    }
}
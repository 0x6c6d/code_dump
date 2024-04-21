#pragma once

#ifndef BLOCKCHAIN_H
#define BLOCKCHAIN_H

#include "Block.h"
#include <vector>

// Blockchain class declaration
class Blockchain
{
private:
    // Attributes
    std::vector<Block> chain;

    // Functions
    Block createGenesisBlock();

public:
    // Constructor
    Blockchain();

    // Functions
    std::vector<Block> getChain();
    Block getLatestBlock();
    bool isChainValid();
    void addBlock(TransactionData data);
    void printChain();
};
#endif
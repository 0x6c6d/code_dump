#pragma once

#ifndef BLOCK_H
#define BLOCK_H

#include "TransactionData.h"
#include <string>
#include <ctime>

using std::string;

// Block class declaration
class Block
{
private:
    int index;
    size_t block_hash;
    size_t previous_hash;
    size_t generateHash();

public:
    // Attributes
    TransactionData transaction_data;

    // Constructor
    Block(int idx, TransactionData td, size_t ph);

    // Functions
    int getIndex();
    size_t getHash();
    size_t getPreviousHash();
    bool isHashValid();
};
#endif
#include "Block.h"
#include "Blockchain.h"
#include <iostream>

// g++ -o bin/sample_blockchain -Iinclude src/Main.cpp src/Blockchain.cpp src/Block.cpp
int main()
{
    // Start blockchain
    Blockchain random_coin;

    // Add blocks to the blockchain
    time_t time1;
    TransactionData td1(4.2, "Bob", "Alice", time(&time1));
    random_coin.addBlock(td1);

    time_t time2;
    TransactionData td2(0.1337, "Günther", "Herman", time(&time2));
    random_coin.addBlock(td2);

    random_coin.printChain();

    return 0;
}
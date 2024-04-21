#ifndef TRANSACTIONDATA_H
#define TRANSACTIONDATA_H

#include <string>
#include <ctime>

// Transaction Data
struct TransactionData
{
    std::string sender_key;
    std::string receiver_key;
    double amount = 0.0;
    time_t timestamp = 0;

    // Default constructor
    TransactionData() = default;

    // Parameterized constructor with initialization list
    TransactionData(double amt, std::string sender, std::string receiver, time_t time)
        : amount(amt), sender_key(sender), receiver_key(receiver), timestamp(time)
    {
    }
};
#endif
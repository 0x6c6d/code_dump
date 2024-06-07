#include <sys/socket.h>
#include <netdb.h>
#include <ifaddrs.h>
#include <stdio.h>
#include <stdlib.h>

// similar to ifconfig / ip addr
// the programs list the IPv4 / IPv6 addresses of the client
int main()
{
    struct ifaddrs *addresses;

    // creates a linked list of addresses
    if (getifaddrs(&addresses) == -1)
    {
        printf("getifaddrs call failes\n");
        return -1;
    }

    // iterate through each address in the linked list
    struct ifaddrs *address = addresses;
    while (address)
    {
        // get the address family
        int family = address->ifa_addr->sa_family;

        // if it isn't IPv4 or IPv6 continue with the next address
        if (family != AF_INET && family != AF_INET6)
        {
            address = address->ifa_next;
            continue;
        }

        printf("%s\t", address->ifa_name);
        printf("%s\t", family == AF_INET ? "IPv4" : "IPv6");

        char ap[100];
        const int family_size = family == AF_INET ? sizeof(struct sockaddr_in) : sizeof(struct sockaddr_in6);
        getnameinfo(address->ifa_addr, family_size, ap, sizeof(ap), 0, 0, 1);
        printf("\t%s\n", ap);

        address = address->ifa_next;
    }

    freeifaddrs(addresses);
    return 0;
}
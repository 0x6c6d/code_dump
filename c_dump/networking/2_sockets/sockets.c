// Executable name   : main
// Version           : 1.0
// Created date      : 02.06.24
// Last updated      : 02.06.24
// Author            : 0x6x6d
// Architecture      : x64
// Platform          : Unix & Windows 
// Description       : The program is a simple TCP based web server which returns the
//                     current time as plain text
//
// use program
// > ./main
// request http://[::1]:8080 or http://127.0.0.1:8080
//

// --- INCLUDE ---
// add the correct includes for Winsock (Windows) / Berkeley (Unix)
#if defined(_WIN32)
#ifndef _WIN32_WINNT
#define _WIN32_WINNT 0x0600
#endif
#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")

#else
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <unistd.h>
#include <errno.h>
#endif

// general includes
#include <stdio.h>
#include <string.h>
#include <time.h>

// --- MACROS ---
#if defined(_WIN32)
// on Windows, socket() returns INVALID_SOCKET if it fails
#define ISVALIDSOCKET(s) ((s) != INVALID_SOCKET) 
// on Windows, closesocket() closes a socket
#define CLOSESOCKET(s) closesocket(s)            
// on windows, the error number can be retrieved by calling WSAGetLastError()
#define GETSOCKETERRNO() (WSAGetLastError())     

#else
#define ISVALIDSOCKET(s) ((s) >= 0) // on Unix, socket() returns a negative number on failure
#define CLOSESOCKET(s) close(s)     // on Unix, close() closes a socket
#define SOCKET int                  // the windows socket() function returns an SOCKET instead of int
#define GETSOCKETERRNO() (errno)    // on unix, the error number gets stored in the thread-global errno variable
#endif

#if !defined(IPV6_V6ONLY)
#define IPV6_V6ONLY 27
#endif

int main()
{
    // windows socket need special initialization
    // Berkley sockets don't need special initialization
#if defined(_WIN32)
    WSADATA d;
    // request Winsock version 2.2
    if (WSAStartup(MAKEWORD(2, 2), &d))
    {
        fprintf(stderr, "Failed to initialize.\n");
        return 1;
    }
#endif

    printf("Configuring local address...\n");
    struct addrinfo hints;
    memset(&hints, 0, sizeof(hints));
    hints.ai_family = AF_INET6;      // IPv6
    hints.ai_socktype = SOCK_STREAM; // TCP
    hints.ai_flags = AI_PASSIVE;     // bind to wildcard address that will be obtained from the getaddrinfo() function below

    struct addrinfo *bind_address;
    getaddrinfo(0, "8080", &hints, &bind_address);

    printf("Creating socket...\n");
    SOCKET socket_listen;
    socket_listen = socket(bind_address->ai_family, bind_address->ai_socktype, bind_address->ai_protocol);
    if (!ISVALIDSOCKET(socket_listen))
    {
        fprintf(stderr, "socket() failed. (%d)\n", GETSOCKETERRNO());
        return 1;
    }

    // remaps IPv4 connection to IPv6 -> dual stack socket
    int option = 0;
    if (setsockopt(socket_listen, IPPROTO_IPV6, IPV6_V6ONLY, (void *)&option, sizeof(option)))
    {
        fprintf(stderr, "setsockopt() failed. (%d)\n", GETSOCKETERRNO());
        return 1;
    }

    printf("Binding socket to local address...\n");
    if (bind(socket_listen, bind_address->ai_addr, bind_address->ai_addrlen))
    {
        fprintf(stderr, "bind() failed. (%d)\n", GETSOCKETERRNO());
        return 1;
    }
    freeaddrinfo(bind_address);

    printf("Listening...\n");
    if (listen(socket_listen, 10) < 0)
    {
        fprintf(stderr, "listen() failed. (%d)\n", GETSOCKETERRNO());
        return 1;
    }

    printf("Waiting for connection...\n");
    // create a new socket for sending data to the client
    // keep the old socket to queue new connection requestes
    struct sockaddr_storage client_address;
    socklen_t client_len = sizeof(client_address);
    SOCKET socket_client = accept(socket_listen, (struct sockaddr *)&client_address, &client_len);
    if (!ISVALIDSOCKET(socket_client))
    {
        fprintf(stderr, "accept() failed. (%d)\n", GETSOCKETERRNO());
        return 1;
    }

    printf("Client is connected... ");
    char address_buffer[100];
    getnameinfo((struct sockaddr *)&client_address, client_len, address_buffer, sizeof(address_buffer), 0, 0, NI_NUMERICHOST);
    printf("%s\n", address_buffer);

    printf("Reading request...\n");
    char request[1024];
    int bytes_received = recv(socket_client, request, 1024, 0);
    printf("Received %d bytes.\n", bytes_received);
    printf("%.*s", bytes_received, request);

    printf("Sending response...\n");
    const char *response =
        "HTTP/1.1 200 OK\r\n"
        "Connection: close\r\n"
        "Content-Type: text/plain\r\n\r\n" // http response header end with blank line
        "Local time is: ";                 // this will be interpreted as plain text by the browser
    int bytes_sent = send(socket_client, response, strlen(response), 0);
    printf("Sent %d of %d bytes.\n", bytes_sent, (int)strlen(response));
    time_t timer;
    time(&timer);
    char *time_msg = ctime(&timer);
    bytes_sent = send(socket_client, time_msg, strlen(time_msg), 0);
    printf("Sent %d of %d bytes.\n", bytes_sent, (int)strlen(time_msg));

    printf("Closing connection...\n");
    CLOSESOCKET(socket_client);

#if defined(_WIN32)
    WSACleanup();
#endif

    printf("Finished.\n");
    return 0;
}

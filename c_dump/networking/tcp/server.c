// Executable name   : server
// Version           : 1.0
// Created date      : 09.06.24
// Last updated      : 09.06.24
// Author            : 0x6x6d
// Architecture      : x64
// Platform          : Unix
// Description       : The program enables communication between a client & a
// server via terminal inputs
//
// use program
// terminal 1: ./server
// termianl 2: ./client 127.0.0.1 50000
// termianl 3: ./client 127.0.0.1 50000
// start writing stuff into terminal 2 or 3 & press enter
//

#include <arpa/inet.h>
#include <errno.h>
#include <netdb.h>
#include <netinet/in.h>
#include <stdio.h>
#include <string.h>
#include <sys/select.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <unistd.h>

int main() {
  int sock_server;
  struct addrinfo hints;
  struct addrinfo *result;

  memset(&hints, 0, sizeof(hints));
  hints.ai_family = AF_INET;
  hints.ai_socktype = SOCK_STREAM;
  hints.ai_flags = AI_PASSIVE;

  int ret = getaddrinfo(0, "50000", &hints, &result);
  if (ret != 0) {
    fprintf(stderr, "[e] getaddrinfo() failed: %s\n", gai_strerror(ret));
    return 1;
  }

  sock_server =
      socket(result->ai_family, result->ai_socktype, result->ai_protocol);
  if (sock_server < 0) {
    fprintf(stderr, "[e] socket() failed: %d\n", errno);
    return 1;
  }

  if (bind(sock_server, result->ai_addr, result->ai_addrlen)) {
    fprintf(stderr, "[e] bind() failed: %d\n", errno);
    return 1;
  }

  freeaddrinfo(result);
  printf("[i] bind() successfully\n");

  if (listen(sock_server, 10) < 0) {
    fprintf(stderr, "[e] listen() failed: %d\n", errno);
    return 1;
  }

  fd_set master;
  FD_ZERO(&master);
  FD_SET(sock_server, &master);
  int sock_max = sock_server;

  printf("[i] Waiting for connections...\n");

  while (1) {
    fd_set reads;
    reads = master;
    if (select(sock_max + 1, &reads, 0, 0, 0) < 0) {
      fprintf(stderr, "[e] select() failed: %d\n", errno);
      return 1;
    }

    int i;
    for (i = 1; i <= sock_max; ++i) {
      if (FD_ISSET(i, &reads)) {
        if (i == sock_server) {
          struct sockaddr_storage addr_client;
          socklen_t addr_len_client = sizeof(addr_client);
          int sock_client = accept(sock_server, (struct sockaddr *)&addr_client,
                                   &addr_len_client);
          if (sock_client < 0) {
            fprintf(stderr, "[e] accept() failed: %d\n", errno);
            return 1;
          }

          FD_SET(sock_client, &master);
          if (sock_client > sock_max) {
            sock_max = sock_client;
          }

          char addr_buffer[100];
          getnameinfo((struct sockaddr *)&addr_client, addr_len_client,
                      addr_buffer, sizeof(addr_buffer), 0, 0, NI_NUMERICHOST);
          printf("[i] New connection from: %s\n", addr_buffer);
        } else {
          char read[1024];
          int bytes_recv = recv(i, read, 1024, 0);
          if (bytes_recv < 1) {
            FD_CLR(i, &master);
            close(i);
            continue;
          }


          int j;
          for (j = 1; j <= sock_max; ++j) {
            if (!FD_ISSET(j, &master)) {
              continue;
            }

            if (j == sock_server || j == i) {
              continue;
            }

            send(j, read, bytes_recv, 0);
          }
        } // else
      }   // if(FD_ISSET())
    }     // for i to sock_max
  }       // while(1)

  close(sock_server);
  printf("[i] Finished\n");
}

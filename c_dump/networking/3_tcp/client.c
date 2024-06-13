// Executable name   : client
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
// > ./client
// start writing stuff into the terminal & press enter
//

#include <arpa/inet.h>
#include <bits/types/struct_timeval.h>
#include <errno.h>
#include <netdb.h>
#include <netinet/in.h>
#include <stdio.h>
#include <string.h>
#include <sys/select.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <unistd.h>

int main(int argc, char *argv[]) {
  int sock;
  struct addrinfo hints;
  struct addrinfo *result;

  if (argc < 3) {
    fprintf(stderr, "[w] usage: client {IP} {PORT}\n");
    return 1;
  }

  memset(&hints, 0, sizeof(hints));
  hints.ai_socktype = SOCK_STREAM;
  int ret = getaddrinfo(argv[1], argv[2], &hints, &result);
  if (ret != 0) {
    fprintf(stderr, "[e] getaddrinfo() failed: %s\n", gai_strerror(ret));
    return 1;
  }

  char address_buffer[100];
  char service_buffer[100];
  getnameinfo(result->ai_addr, result->ai_addrlen, address_buffer,
              sizeof(address_buffer), service_buffer, sizeof(service_buffer),
              NI_NUMERICHOST);
  printf("[i] %s %s\n", address_buffer, service_buffer);

  sock = socket(result->ai_family, result->ai_socktype, result->ai_protocol);
  if (sock < 0) {
    fprintf(stderr, "[e] socket() failed: %d\n", errno);
    return 1;
  }

  if (connect(sock, result->ai_addr, result->ai_addrlen)) {
    fprintf(stderr, "[e] connect() failed: %d\n", errno);
    return 1;
  }

  freeaddrinfo(result);
  printf("[i] Connected successfully\n");
  printf("[i] To send data, enter the text followed by an enter\n");

  while (1) {
    fd_set reads;
    FD_ZERO(&reads);
    FD_SET(0, &reads);
    FD_SET(sock, &reads);

    struct timeval timeout;
    timeout.tv_sec = 0;
    timeout.tv_usec = 100000; // 0.1 seconds

    if (select(sock + 1, &reads, 0, 0, &timeout) < 0) {
      fprintf(stderr, "[e] select() failed: %d\n", errno);
      return 1;
    }

    // check for terminal input from the server
    if (FD_ISSET(sock, &reads)) {
      char read[4096];
      int bytes_received = recv(sock, read, 4096, 0);
      if (bytes_received <= 0) {
        printf("[i] Connection closed by server\n");
        break;
      }
      printf("[i] Received %d bytes: %.*s", bytes_received, bytes_received,
             read);
    }

    // check for terminal input from the client
    if (FD_ISSET(0, &reads)) {
      char read[4096];
      if (!fgets(read, 4096, stdin)) {
        break;
      }

      int bytes_send = send(sock, read, strlen(read), 0);
      printf("[i] Send %d bytes\n", bytes_send);
    }
  }

  close(sock);
  printf("[i] Closed socket\n");

  return 0;
}

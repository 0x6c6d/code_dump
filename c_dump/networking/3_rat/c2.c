#include <arpa/inet.h>
#include <errno.h>
#include <netdb.h>
#include <netinet/in.h>
#include <stdio.h>
#include <string.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <unistd.h>

int main() {
  int sock;
  struct addrinfo ai;
  struct addrinfo *bi;

  // get local address info
  memset(&ai, 0, sizeof(ai));
  ai.ai_family = AF_INET;
  ai.ai_socktype = SOCK_STREAM;
  ai.ai_flags = AI_PASSIVE;
  (void)getaddrinfo(0, "50000", &ai, &bi);

  // create socket
  sock = socket(bi->ai_family, bi->ai_socktype, bi->ai_protocol);
  if (sock < 0) {
    fprintf("[e] socket() error: %d\n", errno);
    return 1;
  }

  // bind to socket
  if (bind(sock, bi->ai_addr, bi->ai_addrlen)) {
    fprintf("[e] bind() error: %d\n", errno);
    return 1;
  }

  freeaddrinfo(bi);

  // listen for incoming connections
  if (listen(sock, 10) > 0) {
    fprintf("[e] listen() error: %d\n", errno);
    return 1;
  }

  // waiting for connection
  printf("[i] waiting for incoming connection");

  // accept connection
  struct sockaddr_storage ss;
  socklen_t sl = sizeof(ss);
  int vic = accept(sock, (struct sockaddr*)&ss, &sl);
  if (vic < 0) {
    fprintf("[e] accept() error: %d\n", errno);
    return 1;
  }
  
  
  return 0;
}

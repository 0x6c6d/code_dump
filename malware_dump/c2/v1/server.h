#ifndef SERVER_H
#define SERVER_H

#include <arpa/inet.h>
#include <errno.h>
#include <fcntl.h>
#include <netdb.h>
#include <netinet/in.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/epoll.h>
#include <sys/select.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <unistd.h>

#define PORT "50000"
#define MAX_EVENTS 10
#define BUFFER_SIZE 1024
#define COMMAND "whoami"

// -- FUNCTIONS --
int fd_set_nonblocking(int fd);

#endif

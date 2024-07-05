#include <stdio.h>
#include <stdlib.h>
#include <uv.h>

#define TARGET_IP "127.0.0.1"
#define TARGET_PORT 9000
#define LOCAL_PORT 8000

typedef struct {
    uv_tcp_t client;
    uv_tcp_t server;
    uv_connect_t connect_req;
    uv_write_t write_req;
    uv_shutdown_t shutdown_req;
    uv_buf_t buffer;
} proxy_conn_t;

void on_close(uv_handle_t* handle) {
    free(handle);
}

void on_shutdown(const uv_shutdown_t* req) {
    // close stream & free handle
    uv_close((uv_handle_t*) req->handle, on_close);
}

void close_connection(proxy_conn_t* proxy) {
    uv_shutdown(&proxy->shutdown_req, (uv_stream_t*) &proxy->client, on_shutdown);
    uv_shutdown(&proxy->shutdown_req, (uv_stream_t*) &proxy->server, on_shutdown);
}

void alloc_buffer(const size_t suggested_size, uv_buf_t* buf) {
    // allocates a buffer
    buf->base = (char*) malloc(suggested_size);
    buf->len = suggested_size;
}

void on_write(uv_write_t* req, const int status) {
    // after uv_write operation complete this method gets called to free allocated memory
    if (status < 0) {
        fprintf(stderr, "Write error: %s\n", uv_strerror(status));
    }
    free(req->bufs->base);
    free(req);
}

void forward_data(uv_stream_t* dest, const ssize_t nread, const uv_buf_t* buf) {
    // handling data that was read from the source (client or server)
    if (nread > 0) {
        // allocates memory for new write request
        uv_write_t* write_req = malloc(sizeof(uv_write_t));
        // initialize write buffer
        const uv_buf_t write_buf = uv_buf_init(buf->base, nread);
        // writes data to destination stream
        uv_write(write_req, dest, &write_buf, 1, on_write);
    }
    // handling end of file or error
    else {
        if (nread < 0) {
            fprintf(stderr, "Read error: %s\n", uv_strerror(nread));
        }
        // closing the stream
        //  on_close callback gets called to free the handle
        uv_close((uv_handle_t*) dest, on_close);

        // frees buffer that gets allocated by the "alloac_buffer" function
        free(buf->base);
    }
}

void on_server_read(const uv_stream_t* server, const ssize_t nread, const uv_buf_t* buf) {
    proxy_conn_t* proxy = server->data;
    // data gets forwarded to the client
    forward_data((uv_stream_t*) &proxy->client, nread, buf);
}

void on_client_read(const uv_stream_t* client, const ssize_t nread, const uv_buf_t* buf) {
    proxy_conn_t* proxy = client->data;
    // data gets forwarded to the server
    forward_data((uv_stream_t*) &proxy->server, nread, buf);
}

void on_connect(const uv_connect_t* req, const int status) {
    // handle connection error
    if (status < 0) {
        fprintf(stderr, "Connection error: %s\n", uv_strerror(status));
        proxy_conn_t* proxy = req->data;
        // closes the client & server stream & frees resources
        close_connection(proxy);
        return;
    }

    // getting pointer to the proxy_conn_t struct we set in the "on_new_connection" function
    proxy_conn_t* proxy = req->data;

    // uv_read_start is used to read data from a stream
    //  calls "alloc_buffer" whenever data is available to allocate space for the incoming data
    //  calls "on_client_read" / "on_server_read" whenever data is available to read
    uv_read_start((uv_stream_t*) &proxy->client, alloc_buffer, on_client_read);
    uv_read_start((uv_stream_t*) &proxy->server, alloc_buffer, on_server_read);
}

void on_new_connection(uv_stream_t* server, const int status) {
    // if status of new connection is negative an error occured
    if (status < 0) {
        fprintf(stderr, "New connection error: %s\n", uv_strerror(status));
        return;
    }

    // allocating memory for proxy_conn_t struct
    //  holds client & server tcp handle + infos to manage the connection
    proxy_conn_t* proxy = malloc(sizeof(proxy_conn_t));
    // initializes tcp client & server handles
    //  both handles are inside the same event loop as the server handle in the main function
    uv_tcp_init(server->loop, &proxy->client);
    uv_tcp_init(server->loop, &proxy->server);

    // uv_accept accepts the incoming client connection
    // if successful, uv_accept initializes the &proxy->client with the accepted connection
    //  server: pointer to the server stream (tcp handle from main function)
    //  &proxy->client: pointer to the client stream (tcp handle in the proxy struct
    //                                              which represents the accepted connection)
    if (uv_accept(server, (uv_stream_t*) &proxy->client) == 0) {
        // define ip address and port of target server
        struct sockaddr_in dest;
        uv_ip4_addr(TARGET_IP, TARGET_PORT, &dest);

        // initiates the connection to the target server
        //  on_connect is the callback that gets called when a connection is establisehd
        uv_tcp_connect(&proxy->connect_req, &proxy->server, (const struct sockaddr*) &dest, on_connect);
        // pointer of proxy_conn_t struct is stored for later use
        proxy->connect_req.data = proxy;
        proxy->client.data = proxy;
        proxy->server.data = proxy;
    } else {
        // handle connection failure
        //  client handle will be closed
        uv_close((uv_handle_t*) &proxy->client, on_close);
    }
}

int main(void) {
    uv_loop_t *loop = uv_default_loop();
    // decalares tcp handle
    uv_tcp_t server;
    // initializes above tcp handle
    //  tcp handle is a structure that represents a tcp stream
    //  provides context for libuv to manage the servers lifecyle & I/O
    uv_tcp_init(loop, &server);

    // define ip address and port of local proxy server
    struct sockaddr_in addr;
    uv_ip4_addr("0.0.0.0", LOCAL_PORT, &addr);

    // above tcp handle is bound to defined ip address and port
    //  prepares server to listen for incoming connections on the defined address & port
    uv_tcp_bind(&server, (const struct sockaddr*) &addr, 0);

    // when client connects the callback "on_new_connection" is triggered
    const int r = uv_listen((uv_stream_t*) &server, 128, on_new_connection);
    if (r) {
        fprintf(stderr, "Listen error: %s\n", uv_strerror(r));
        return 1;
    }

    // starts the event loop
    //  making it possible to handle events (e.g. incoming connections)
    return uv_run(loop, UV_RUN_DEFAULT);
}

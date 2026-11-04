# code_dump

A collection of small sample projects, exercises and experiments in **Assembly, C, C++, C#, Go, JavaScript / TypeScript and Rust**.

Each folder is self-contained. The aim is to learn how things work close to the metal (syscalls, sockets, bootloaders, memory) as well as how they are built at the top of the stack (web APIs, frontends, architecture patterns).

> Many of the C#, JS/TS and Rust projects were built by following tutorials or books. Their own READMEs say where they come from. They are practice projects, not production code.

## Contents

| Language | Folder | Focus |
| --- | --- | --- |
| Assembly | [`assembly_dump`](assembly_dump) | x86 / x64 NASM: registers, syscalls, stack, small tools |
| C | [`c_dump`](c_dump) | Pointers, sockets, TCP, an async proxy, a bootloader, a neural network |
| C++ | [`cpp_dump`](cpp_dump) | Berkeley sockets, templates, a toy blockchain |
| C# | [`csharp_dump`](csharp_dump) | ASP.NET, Blazor, JWT auth, clean architecture, vertical slicing |
| Go | [`go_dump`](go_dump) | A minimal HTTP API |
| JS / TS | [`js_dump`](js_dump) | Next.js 13 apps, React + .NET CRUD |
| Rust | [`rust_dump`](rust_dump) | CLI tools, async port scanner, EVM bytecode parser, Advent of Code |

---

## Assembly

NASM, Linux. The x86 examples are 32-bit and use `int 0x80` syscalls. The x64 programs use `syscall` and are headed by a short comment block with build and usage notes.

**x86** (`assembly_dump/x86`)

| File | What it does |
| --- | --- |
| `ex1.asm` | Basic arithmetic with registers: `mov`, `add`, `sub`, `mul`, `div` |
| `ex2.asm` | Prints "Hello World" with the `sys_write` syscall |
| `ex3.asm` | A `for`-style loop with a counter in `ecx` |
| `ex4.asm` | Changes byte values of a string in the data section |
| `ex5.asm` | Builds a string on the stack with `sub esp` and writes it out |
| `ex6.asm` | Defines and calls simple functions |

**x64** (`assembly_dump/x64`)

| File | What it does |
| --- | --- |
| `1_basis_operations/program.asm` | Sandbox for basic operations and tricks (`xor rax, rax`, addressing) |
| `2_stdout/gcc-linker.asm` | "Hello World" via `syscall`, linked with gcc |
| `2_stdout/ld-linker.asm` | The same program, linked directly with `ld` |
| `3_small-programs/1_uppercase_v1.asm` | Reads stdin and converts all letters to uppercase |
| `3_small-programs/2_uppercase_v2.asm` | Second version of the uppercase filter |
| `3_small-programs/3_hexdump.asm` | Prints the input as hexadecimal values |
| `3_small-programs/4_translation-table.asm` | Translates bytes through a lookup table |

## C

**Basics** (`c_dump/basics`)

| File | What it does |
| --- | --- |
| `1_function_pointer.c` | Passes a predicate as a function pointer to a filter function |

**Networking** (`c_dump/networking`)

| Folder | What it does |
| --- | --- |
| `ifconfig/ifconfig.c` | Lists the IPv4 / IPv6 addresses of the host, like `ifconfig` / `ip addr` |
| `sockets/sockets.c` | A simple TCP web server that returns the current time as plain text (Unix & Windows) |
| `tcp/server.c`, `tcp/client.c` | A terminal chat between a TCP server and client |
| `proxy/main.c` | A TCP proxy on top of **libuv** that forwards local port 8000 to 127.0.0.1:9000 |

**Machine learning** (`c_dump/ml/nn_xor`)

| File | What it does |
| --- | --- |
| `xor.c` | A neural network written from scratch (2-2-1) that learns XOR with backpropagation. The README shows the training output. |

**Bootloader** (`c_dump/bootloader`)

A small hobby OS that boots from a FAT12 floppy image in QEMU, following an OS-dev video series.

| File | What it does |
| --- | --- |
| `src/bootloader/boot.asm` | 16-bit boot sector with a FAT12 BIOS Parameter Block |
| `src/kernel/main.asm` | Minimal kernel loaded at `0x7C00`, prints text via BIOS |
| `Makefile` | Builds the floppy image with `nasm` and `mtools` |

## C++

**Networking** (`cpp_dump/network`), based on [Beej's Guide to Network Programming](https://beej.us/guide/bgnet/html/)

| File | What it does |
| --- | --- |
| `1_host_to_ip.cpp` | Resolves a host name to its IP addresses |
| `2.1_stream_server.cpp` | TCP stream socket server |
| `2.2_stream_client.cpp` | TCP stream socket client |
| `3.1_dgram_listener.cpp` | UDP datagram listener |
| `3.2_dgram_talker.cpp` | UDP datagram sender |

**Math** (`cpp_dump/math`)

| File | What it does |
| --- | --- |
| `vector.h`, `vector.cpp` | A templated `Vector<T, N>` class with an initializer-list constructor and size checks |

**Crypto** (`cpp_dump/crypto/sample_blockchain`)

| File | What it does |
| --- | --- |
| `include/TransactionData.h` | Transaction data structure |
| `include/Block.h`, `src/Block.cpp` | A block with hash and previous-hash link |
| `include/Blockchain.h`, `src/Blockchain.cpp` | The chain: adding and validating blocks |
| `src/Main.cpp` | Demo that builds a small chain |

## C#

All projects are .NET solutions (`.sln`). Open them in Visual Studio or Rider, or build with `dotnet`.

| Project | What it is |
| --- | --- |
| `tutorial-customer-signup-form` | ASP.NET MVC sign-up form for tablets that captures a signature. Custom validation attributes (mail, adult, country, checkbox, signature) and a data library for SQL access. |
| `tutorial-custom-login-api` | Auth API with password hash and salt, **JWT**, refresh tokens and roles. Split into API, business logic (BLL) and data (DAL) layers, backed by SQLite. |
| `tutorial-ecommerce-app` | Blazor WebAssembly shop (.NET 6) with custom login, Stripe payments and an admin dashboard for products and categories. |
| `tutorial-suggestion-app` | Blazor Server app where users post and vote on suggestions and an admin reviews them. Uses a shared library for data access. |
| `tutorial-clean-architecture` | *WarehouseManager*: **clean architecture** with Domain, Application, Infrastructure, Persistence, Api, Web (Blazor) and an Api.Test project. EF Core migrations and Swagger included. |
| `tutorial-vertical-slicing` | A warehouse dashboard organised as **vertical slices** (Application and Presentation). |

## Go

| File | What it does |
| --- | --- |
| `sample_api/cmd/sample_api/main.go` | Entry point of the HTTP server |
| `sample_api/internal/routes/routes.go` | Router with `/` and `/api/data` handlers on `net/http` |
| `sample_api/go.mod`, `makefile` | Module definition (Go 1.22) and build targets |

## JavaScript / TypeScript

| Project | What it is |
| --- | --- |
| `tutorial-meetup-app` | Meetup site in **Next.js 13** (app directory) with Tailwind: header, meetup list and cards. |
| `tutorial-news-app` | Responsive news site in **Next.js 13** with the mediastack API through a StepZen GraphQL layer, dark mode, search and live timestamps. |
| `tutorial-linktree` | Linktree clone with a Next.js frontend (`ui`) and a .NET 7 backend (`api`). |
| `tutorial-dotnet-api-react-frontend` | React frontend for managing posts, backed by an ASP.NET Core CRUD API. |

## Rust

| Project | What it is |
| --- | --- |
| `cheat_sheet` | Notes in code: stack vs. heap, copy vs. move, ownership |
| `number_guesser` | The classic guessing game (`rand`, `Ordering`) |
| `fibonacci` | CLI that prints the nth Fibonacci number, argument parsing with `bpaf` |
| `port_sniffer` | Multithreaded async TCP port scanner (`tokio`, channels, `bpaf`) |
| `windows` | Calls the Win32 thread pool API through the `windows` crate |
| `evm-opcode-parser` | CLI (`clap`) that analyses Solidity contract bytecode. It splits metadata from code and lists opcodes. A control-flow graph is planned, based on the EtherSolve paper. |
| `aoc_2023` | Advent of Code 2023, days 1 and 2, parts 1 and 2 as separate binaries |
| `rustlings` | Copy of the [rustlings](https://github.com/rust-lang/rustlings) exercise runner with a few exercises |

---

## Building and running

Each project needs only its own toolchain.

```sh
# Assembly (x64, ld)
nasm -f elf64 -g -F dwarf program.asm && ld -o program program.o

# C
gcc main.c -lm -o main

# C++
g++ 2.1_stream_server.cpp -o server

# Go
cd go_dump/sample_api && go run ./cmd/sample_api

# Rust
cd rust_dump/<project> && cargo run

# JS / TS
cd js_dump/<project> && npm install && npm run dev

# C#
dotnet run --project <project>.csproj
```

Check a project's own `README.md` for extra requirements, such as `qemu` and `mtools` for the bootloader, `libuv` for the proxy, or API keys for the news app.

## Repository layout

```text
code_dump/
├── assembly_dump/   x86 and x64 NASM examples
├── c_dump/          basics, networking, ml, bootloader
├── cpp_dump/        network, math, crypto
├── csharp_dump/     ASP.NET / Blazor tutorial projects
├── go_dump/         sample_api
├── js_dump/         Next.js and React projects
└── rust_dump/       CLI tools, parsers, exercises
```

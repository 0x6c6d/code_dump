# OS

Bases on this video series: https://www.youtube.com/watch?v=9t-SPC7Tczc&list=PLFjM7v6KGMpiH2G-kT781ByCNC_0pKpPN

<br>

## Requirements

- nasm (assembler)
- Make (Makefile)
- qemu (or other virutalization software)
- mcopy (yay -S mtools)
- mkfs.fat (yay -S dosfstools)
- bocks (debugger - yay -S bochs)

<br>

## Building & Starting

Build:
- cd into project directory and call `make`

Start:
- cd into project build folder after calling `make`
- cd into /build -> `qemu-system-i386 -fda main_floppy.img`

<br>

## Basic Information

#### Computer Startup

1. BIOS is copied from ROM (read-only memory) chip into RAM
2. BIOS starts executing code
  - hardware initialization
  - POST (power-on self test)
3. BIOS searches for OS
  - Legacy Booting
    - BIOS loads first sector (512 bytes) of each bootable device into memory (at location 0x7C00)
    - BIOS checks for 0xAA55 signature (byte 511 & 512)
    - starts executing code if signature is found
  - EFI
    - BIOS looks into secial EFI partitions
    - OS must be compiled as EFI file
4. Bootloader loads basic components
  - fits into first sector (512 bytes)
  - switches system from 16bit into 32bit / 64bit


#### Bootloader

- consists of code and data
- data
  - BPB (BIOS paramter block)
    - default stuff for the FAT file system
  - extendend boot record
    - FAT 12 specific data
- code
  - conversion from CHS to LBA -> BIOS only supports CHS addressing 
    - CHS (cylinder-head-sector): old school disk layout (floppy disk, HDD) to determine were data is physicaly on the disk -> needs 3 numbers to find data on the disk (for cylinder, head and sector)
    - LBA (logical block addressing): linear addressing scheme -> data is located by an index
    - conversion math:
      - sector = (LBA % sectors per track) + 1
      - head = (LBA / sectors per track) % heads per cylinder
      - cylinder (LBA / sectors per track) / heads per 
      

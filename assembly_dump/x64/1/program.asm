; Executable name   : EATSYSCALL
; Version           : 1.0
; Created date      : 17.05.24
; Last updated      : 17.05.24
; Author            : 0x6x6d
; Architecture      : x64
; Description       : Program to demonstrate the syscall instruction
; IDE               : VS Code
;
;
; Build using these commands;
; > nasm -­f elf64 -­g -­F dwarf program.asm
; > ld -o program program.o

section .data                       ; contains initialized data
    Msg: db "Hello World!", 0x0A
    MsgLen: equ $-Msg

section .text
global _start               ; ld linker needs this to find entry point
_start:
    ; write to console
    mov rax, 1              ; 1 = sys_write
    mov rdi, 1              ; 1= fd for stdout
    mov rsi, Msg            ; put address of msg in rsi
    mov rdx, MsgLen         ; length of string to be written
    syscall                 ; make system call

    ; exit console
    mov rax, 60             ; 60 = exit program
    mov rdi, 0              ; return 0 = success
    syscall
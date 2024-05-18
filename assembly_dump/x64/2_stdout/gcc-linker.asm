; Executable name   : EATSYSCALL
; Version           : 1.0
; Created date      : 17.05.24
; Last updated      : 17.05.24
; Author            : 0x6x6d
; Architecture      : x64
; Description       : Program to demonstrate the syscall instruction
; IDE               : SASM -> uses gcc as linker

section .data                       ; contains initialized data
    Msg: db "Hello World!", 0x0A    ; variable names represent addresses & not the data itself
    MsgLen: equ $-Msg

section .text
global main                 ; gcc linker needs this to find entry point
                            ; like a c program without writing c -> has to start with main
main:
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
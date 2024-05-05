; print out a msg in the console

global _start

section .data
    msgOne db "Hello World", 0x0a      ; 0x0a = 10 -> code for newline character
    len equ $ - msgOne                 ; "$" = offset from the beginning of the section
                                    ; "msg" = offset of the msg first byte
    
section .text
_start:
    mov eax, 4      ; sys_write
    mov ebx, 1      ; stdout fd (file descriptor)
    mov ecx, msgOne    ; bytes to write
    mov edx, len    ; number of bytes to write
    int 0x80        ; perform syscall

    mov eax, 1      ; sys_exit
    mov ebx, 0      ; status code: success
    int 0x80   
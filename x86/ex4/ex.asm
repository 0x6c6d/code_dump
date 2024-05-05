global _start
    
section .data
    ; db = 1 byte
    ; dw = 2 bytes
    ; dd = 4 bytes
    addr db "yellow", 0x0a      ; addr label is pointer to memory addr
    addrLen equ $ - addr

section .text
_start:
    mov [addr], byte "H"    ; addr pointer points to the first byte of "yellow"
                            ; "y" will be changed to "H"
    mov [addr+5], byte '!'  ; addr+5 points to "w"
    mov eax, 4              ; sys_write
    mov ebx, 1              ; stdout
    mov ecx, addr           ; bytes to write
    mov edx, addrLen        ; number of bytes to write
    int 0x80                ; syscall
    jmp exit

exit:
    mov eax, 1          ; sys_exit
    mov ebx, 0          ; success status
    int 0x80            ; syscall

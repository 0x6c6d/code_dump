; for loop

global _start
    
section .data
    msgOne db 0        ; db = define-byte -> msg var has the size of one byte

section .text
_start:
    mov ecx, 1      ; start value
    mov edx, 65     ; 65 iterations
loop:
    add ecx, 1      ; ecx += 1
    dec edx         ; edx -= 1
    cmp edx, 1      ; compare edx to 1
    jg loop         ; if edx > 1 jump to start of loop
    jmp exit        ; jump to exit

exit:
    ; print value of ecx
    mov eax, 4          ; sys_write
    mov ebx, 1          ; file descriptor for stdout
    mov [msgOne], ecx
    mov ecx, msgOne
    mov edx, 4          ; length of msg
    int 0x80            ; syscall

    ; exit
    mov eax, 1          ; sys_exit
    xor ebx, ebx        ; status code = success
    int 0x80            ; syscall

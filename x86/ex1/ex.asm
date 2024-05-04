; global: makes identifier (_start) accessible to the linker
global _start

; identifiert + ":" creates a label -> used to name locations in the code
_start:
    mov ebx, 123        ; ebx = 123
    mov eax, ebx        ; eax = ebx
    add ebx, eax        ; ebx += eax (123)
    sub ebx, edx        ; ebx -= edx
    mul ebx             ; eax *= ebx
    div edx             ; eax /= ebx
    
    mov eax, 1          ; sys_exit
    mov ebx, 0          ; exit status code: 0 = exit success
    int 0x80            ; 0x80 = interrupt handler for system calls
; push bytes to the stack

global _start

_start:
    ; push bytes to stack
    sub esp, 4                  ; allocate 4 bytes
    mov [esp], byte "H"         ; moves bytes into allocated space
    mov [esp+1], byte "i"
    mov [esp+2], byte "!"
    mov [esp+3], byte 0x0a
    ; write to stdout
    mov eax, 4
    mov ebx, 1
    mov ecx, esp
    mov edx, 4
    int 0x80
    jmp exit


exit:
    mov eax, 1      ; sys_exit
    mov ebx, 0      ; success status
    int 0x80        ; syscall

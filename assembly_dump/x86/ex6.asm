; simple function

global _start

section .data
    msgOne db "Function One!", 0x0a
    msgTwo db "Function Two!", 0x0a
    msgOneLen equ $ - msgOne
    msgTwoLen equ $ - msgTwo

section .text
_start:
    call func_one       ; pushes return location of the next call ("jmp exit") onto the stack
    call func_two
    jmp exit

func_one:
    mov eax, 4
    mov ebx, 1
    mov ecx, msgOne
    mov edx, msgOneLen 
    int 0x80
    pop eax             ; moves return location from "call func" into eax
    jmp eax             ; return to "jmp exit"

func_two:
    mov eax, 4
    mov ebx, 1
    mov ecx, msgTwo
    mov edx, msgTwoLen 
    int 0x80
    ret                 ; replaces "pop eax" "jmp eax"

exit:
    mov eax, 1
    mov ebx, 0
    int 0x80
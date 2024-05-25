; Executable name   : uppercase
; Version           : 1.0
; Created date      : 20.05.24
; Last updated      : 20.05.24
; Author            : 0x6x6d
; Architecture      : x64
; Description       : The program reads an input file and creates an output file 
;                     in which all letters are converted to uppercase.
; IDE               : SASM -> uses gcc as linker
;
; use program
; > ./uppercase > outputfile < inputfile  
;

section .bss
    Buff resb 1             ; reserves 1 byte of space for variable "Buff"
    
section .data

section .text
global main

main:
    mov rbp, rsp            ; for correct debugging
    
Read:
    ; read character for character
    mov rax, 0              ; 0 = sys_read
    mov rdi, 0              ; 0 = fd stdin
                            ; keeps track of the current position -> each sys_read call will read a new character (no manual looping needed)
    mov rsi, Buff           ; address of the buffer to read to
    mov rdx, 1              ; tell sys_read to read one char from stdin
    syscall
    
    cmp rax, 0              ; compare sys_reads return value with EOF char (0)
    je Exit                 ; jumpt to "Exit" if EOF is reached
    
    ; check if character is lowercase letter
    cmp byte [Buff], 61h    ; compare char against "a"
    jb Write                ; if "[Buff]" is below "61h" which means no lowercase letter jump to write
    cmp byte [Buff], 7Ah    ; compare char against "z"
    ja Write                ; if "[Buff]" is above "7Ah" which means no lowercase letter jump to write
    
    ; convert lowercase to uppercase char
    sub byte [Buff], 20h    ; subtract 20h to get uppercase char
    
Write:
    mov rax, 1              ; 1 = sys_write
    mov rdi, 1              ; 1 = fd stdout
    mov rsi, Buff           ; address of the char to write
    mov rdx, 1              ; number of chars to write
    syscall
    jmp Read                ; get the next char
    
Exit:
    ret                     ; end program

 
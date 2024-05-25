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
    BUFFLEN equ 128         ; set buffer length to 128
    Buff    resb BUFFLEN    ; create uninitialized buffer
    
section .data

section .text
global main

main:
    mov rbp, rsp                ; for correct debugging

Read:  
    ; read text into buffer
    mov rax, 0                  ; 0 = sys_read
    mov rdi, 0                  ; 0 = fd stdin
    mov rsi, Buff               ; address of the buffer to read to
    mov rdx, BUFFLEN            ; tell sys_read to read one char from stdin
    syscall
    mov r12, rax                ; mov return value to r12 for later use -> to indicate how much bytes to write
    cmp rax, 0                  ; check for EOL
    je Exit
    
    ; set up register to iterate over buffer
    mov rbx, rax                ; rax contains number of bytes read -> place it into rbx
    mov r13, Buff               ; place address of buffer into r13
    dec r13                     ; r13 + rbx would be one off -> adjust offest by one
    
Scan:
    ; iterate through buffer and convert lowercase letters to uppercase ones
    cmp byte [r13+rbx], 61h     ; compare char against "a"
    jb .Next                    ; if char is below "61h" which means no lowercase letter jump to write
    cmp byte [r13+rbx], 7Ah     ; compare char against "z"
    ja .Next                    ; if charis above "7Ah" which means no lowercase letter jump to write
    
    ; convert lowercase to uppercase char
    sub byte [r13+rbx], 20h        ; subtract 20h to get uppercase char

.Next:
    dec rbx                     ; decrement rbx to iterate over the next character
    cmp rbx, 0                  ; compare rbx with 0 to check if the loop is over
    jnz Scan         
  
Write:
    mov rax, 1                  ; 1 = sys_write
    mov rdi, 1                  ; 1 = fd stdout
    mov rsp, Buff               ; write the whole buffer
    mov rdx, r12                ; length of buffer to write
    syscall
    jmp Read                    ; jump to read to read the next buffer 
                                
Exit:
    ret                         ; exit program
; Executable name   : translation table
; Version           : 1.0
; Created date      : 25.05.24
; Last updated      : 25.05.24
; Author            : 0x6x6d
; Architecture      : x64
; Description       : The program reads an input file and translates the values based on the defined translation table
; IDE               : SASM -> uses gcc as linker
;
; use program
; > ./translation-table < inputfile  
;

section .data
TranslationTable:
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,09h,0Ah,20h,20h,20h,20h,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    db 20h,21h,22h,23h,24h,25h,26h,27h,28h,29h,2Ah,2Bh,2Ch,2Dh,2Eh,2Fh
    db 30h,31h,32h,33h,34h,35h,36h,37h,38h,39h,3Ah,3Bh,3Ch,3Dh,3Eh,3Fh
    db 40h,41h,42h,43h,44h,45h,46h,47h,48h,49h,4Ah,4Bh,4Ch,4Dh,4Eh,4Fh
    db 50h,51h,52h,53h,54h,55h,56h,57h,58h,59h,5Ah,5Bh,5Ch,5Dh,5Eh,5Fh
    db 60h,61h,62h,63h,64h,65h,66h,67h,68h,69h,6Ah,6Bh,6Ch,6Dh,6Eh,6Fh
    db 70h,71h,72h,73h,74h,75h,76h,77h,78h,79h,7Ah,7Bh,7Ch,7Dh,7Eh,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    db 20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h,20h
    
section .bss
    BUFFLEN     equ 1024
    Buffer      resb BUFFLEN
    
section .text
global main

main:
    mov rbp, rsp                ; for debugging in SASM
    
read:
    ; read buffer from stdin
    mov rax, 0                  ; 0 = sys_read
    mov rdi, 0                  ; 0 = fd stdin
    mov rsi, Buffer             ; Buffer to write to
    mov rdx, BUFFLEN            ; length to write to buffer
    syscall
    mov rbp, rax                ; save return value for later
    cmp rax, 0                  ; compare return value (0 = end of stdin)
    je exit                     ; jump to exit when all characters are read
    
    ; set up register for translation
    mov rbx, TranslationTable   ; place address of TranslationTable into register
    mov rdx, Buffer             ; place address of Buffer into register
    mov rcx, rbp                ; place number of bytes read into register
    
translate:
    ; translate buffer data based on translation table
    xor rax, rax                ; clear rax
    mov al, byte [rdx-1+rcx]    ; load character into al (8 bit)
    xlat                        ; translates charcter placed in al based on translation table in rbx
    mov byte [rdx-1+rcx], al    ; place translated byte back into buffer
    dec rcx                     ; decrement character count (we start at the "highest" address because of little endian)
    jnz translate
    
    ; print out translated characters
    mov rax, 1                  ; 1 = sys_write
    mov rdi, 1                  ; 1 = fd stdout
    mov rsi, Buffer             ; buffer to print out
    mov rdx, rbp                ; length of buffer to print out
    syscall
    
exit:
    ret                         ; exit the program
        
    
    
    
    
    
    
    
    
    
    
    

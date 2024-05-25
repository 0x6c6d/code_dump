; Executable name   : hexdump
; Version           : 1.0
; Created date      : 24.05.24
; Last updated      : 24.05.24
; Author            : 0x6x6d
; Architecture      : x64
; Description       : The program reads an input file and outputs the values as hexadecimal characters
; IDE               : SASM -> uses gcc as linker
;
; use program
; > ./hexdump < inputfile  
;

section .bss
    BUFFLEN equ 16          ; 1 hex char = 16 bits
    Buff    resb BUFFLEN    ; create uninitialized buffer

section .data
    Digits: db "0123456789ABCDEF"   ; lookup table
    HexStr: db " 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 0x0A
    HEXLEN: equ $-HexStr

section .text
global main

main: 
    mov rbp, rsp            ; SASM needs this for debugging
    
Read:
    ; read input text into buffer
    mov rax, 0              ; sys_read
    mov rdi, 0              ; 0 = fd stdin
    mov rsi, Buff           ; address of buffer to read to
    mov rdx, BUFFLEN        ; length of bits to read
    syscall
    mov r15, rax            ; safe number of bytes in the buffer for later
    cmp rax, 0              ; 0 = EOL
    je Exit
    
    ; set up register to process the buffer
    mov rsi, Buff                   ; mov address of buffer into rsi
    mov rdi, HexStr                 ; mov address of HexStr into rdi
    xor rcx, rcx                    ; clear line string pointer to 0
    
    ; go through buffer & convert binary values into hex
Scan:
    xor rax, rax                    ; clear rax
    
    ; calculate offset into line string, which is rcx * 3 (3 because of space + 2 digit hex value [ AB] -> [ AB 12 FB 3C...])
    mov rdx, rcx                    ; move line string pointer into rdx
    shl rdx, 1                      ; shift bits one to the left -> it's like multiplying the pointer by 2
    add rdx, rcx                    ; complete the multiplication
    
    ; get char from buffer and put it into rax and rbx
    mov al, byte [rsi+rcx]          ; [rsi+rcx] = value of [Buff address + offset]
    mov rbx, rax
    
    ; look up low nybble & insert it into string
    and al, 0Fh                     ; 1011 1010 (example byte)
                                    ; 0000 1111 (mask = 0Fh)
                                    ; 0000 1010 (result)
    mov al, byte [Digits+rax]       ; look up char equivalent of nybble
    mov byte [HexStr+rdx+2], al     ; write char value into HexStr
    
    ; look up high nybble & insert it into string
    shr bl, 4                       ; 1011 1010 (example byte)
                                    ; 0000 1011 (result)
    mov bl, byte [Digits+rbx]       ; look up char equivalent of nybble
    mov byte [HexStr+rdx+1], bl     ; write char value into HexStr
    
    ; see if the end of the buffer is reached
    inc rcx                         ; increment buffer pointer
    cmp rcx, r15                    ; compare number of chars in buffer
    jna Scan                        ; loop back if rcx <= number of chars in buffer
    
    ; Print out hex values
    mov rax, 1                      ; sys_write
    mov rdi, 1                      ; 1 = fd std_out
    mov rsi, HexStr                 ; bytes to write
    mov rdx, HEXLEN                 ; length of bytes to write
    syscall
    jmp Read
    
Exit:
    ret                             ; exit the program
    

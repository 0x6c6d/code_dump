section .data
    MyVar: db "HELLOWORLD"

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    
    ; --- tips & tricks ---
    xor rax, rax        ; puts 0 into rax
    
    ; --- sandbox ---
    ; switch  bytes manuel vs xchg operation
    mov rax, 067ABh
    mov rbx, rax
    xor rcx, rcx
    mov ch, bl
    mov cl, bh
    xchg cl, ch
    
    ; read / write from memory
    xor rax, rax
    xor rbx, rbx
    mov rax, MyVar
    mov bl, [MyVar]
    mov byte [MyVar], "Y"
    mov bl, [MyVar]
    
    ; increment / decrement
    mov ebx, 02Dh
    dec ebx                 ; clear a lot of flags
    mov eax, 0FFFFFFFFh      
    inc eax                 ; rollover happens -> sets a lot of flags
    
    ; jump if not zero
    mov rax, 10
    mov rbx, MyVar
Repeat:
    add byte [rbx], 32      ; converts uppercase letters into lowercase letters
    inc rbx
    dec rax
    jnz Repeat              ; checks if ZF is set -> if it is not set the code jumps back to "Repeat:"
    
    ; negative values
    mov eax, 9
    neg eax                ; negative of 9 != -9 it is 0xfffffff7 -> two's complement
    add eax, 42
    
    ; moving signed values aroung
    mov ax, -9
    mov rbx, rax            ; this doesn't work -> the sign just turns to a normal bit
    mov cx, -9
    movsx rdx, cx           ; this is the correct operation to mov signed values
    
    ; multiplication
    mov eax, 447
    mov ebx, 1337
    mul ebx                 ; multiplies ebx with value placed in eax
    mov eax, 0FFFFFFFFh
    mov ebx, 03B72h
    mul ebx                 ; overflow can happen when to large are multiplied, e.g. 16bit * 16bit can result in numbers up to 32bit
                            ; second register is used to hold the part of the number that doesnt fit into the first register
                            ; if this happens the OF (overflow flag) will be set
                            
    ; devision
    mov rdx, 0              ; must be set to zero to store the remainder
    mov rax, 251            ; Dividend
    mov rbx, 5              ; Divisor
    div rbx
    
    ; --- exit program ---
    mov rax, 60
    mov rdi, 0
    syscall
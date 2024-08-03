; org tells assembler where our code will be loaded into memory
; assembler uses this info to calculate label addresses offset
org 0x7C00 

; tells assembler to emit 16 bit code
; this is needed for backwards compatibility (first x86 CPU (Intel 8086) used 16 bit)
bits 16

%define ENDL 0x0D, 0x0A

start:
        jmp main

; -- Function
; Prints a string to the screen
; Params:
;     - ds:si -> points to string
puts:
        ; save registers
        push si
        push ax
        push bx

.loop:
        lodsb           ; load next character in al
        or al, al       ; check if next character is null -> sets zero flag if null
        jz .done        ; if zero flag is set jump to .done

        mov ah, 0x0E    ; call bios interrupt -> write character in TTY Mode 
        mov bh, 0       ; set page number to 0
        int 0x10        ; call bios interrupt -> 0x10 is video chategory
        
        jmp .loop       ; else continue the loop

.done:
        pop bx
        pop ax
        pop si
        ret


main:
        ; setup data segments
        mov ax, 0  
        mov ds, ax
        mov es, ax

        ; setup stack
        mov ss, ax
        mov sp, 0x7C00 ; stack grows downwards 

        ; print text to bios
        mov si, msg_string
        call puts
        
        hlt
        
.halt:
        jmp .halt


msg_string: db 'Hello world', ENDL, 0
        

; in legacy booting mode the BIOS expects that the last to bytes of the first sector are 0xAA55
; on sector has 512 bytes
; 510 - length of program -> used to pad the program to a size of 510
times 510-($-$$) db 0
; add signature to byte 511 & 512
dw 0AA55h

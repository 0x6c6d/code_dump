; org tells assembler where our code will be loaded into memory
; assembler uses this info to calculate label addresses offset
org 0x7C00 

; tells assembler to emit 16 bit code
; this is needed for backwards compatibility (first x86 CPU (Intel 8086) used 16 bit)
bits 16

%define ENDL 0x0D, 0x0A

;
; FAT12 header (BPB = BIOS Parameter Block)
; https://wiki.osdev.org/FAT#BPB_(BIOS_Parameter_Block)
; 
jmp short start
nop

bdb_oem:                    db 'MSWIN4.1'           ; 8 bytes
bdb_bytes_per_sector:       dw 512
bdb_sectors_per_cluster:    db 1
bdb_reserved_sectors:       dw 1
bdb_fat_count:              db 2
bdb_dir_entries_count:      dw 0E0h
bdb_total_sectors:          dw 2880                 ; 2880 * 512 ~ 1.4MB
bdb_media_descriptor_type:  db 0F0h                 ; F0 = 3.5" floppy disk
bdb_sectors_per_fat:        dw 9                    ; 9 sectors/fat
bdb_sectors_per_track:      dw 18
bdb_heads:                  dw 2
bdb_hidden_sectors:         dd 0
bdb_large_sector_count:     dd 0

; extended boot record
ebr_drive_number:           db 0                    ; 0x00 floppy, 0x80 hdd, useless
                            db 0                    ; reserved
ebr_signature:              db 29h
ebr_volume_id:              db 78h, 56h, 34h, 12h   ; serial number, value doesn't matter
ebr_volume_label:           db 'CUSTOM OS  '        ; 11 bytes, padded with spaces
ebr_system_id:              db 'FAT12   '           ; 8 bytes


;
; Bootloader beginning
;
start:
        jmp main



;
; Prints a string to the screen
; Params:
;   - ds:si points to string
;
puts:
    ; save registers we will modify
    push si
    push ax
    push bx

.loop:
    lodsb               ; loads next character in al
    or al, al           ; verify if next character is null?
    jz .done

    mov ah, 0x0E        ; call bios interrupt
    mov bh, 0           ; set page number to 0
    int 0x10

    jmp .loop

.done:
    pop bx
    pop ax
    pop si    
    ret


main:
    ; setup data segments
    mov ax, 0                   ; can't set ds/es directly
    mov ds, ax
    mov es, ax
    
    ; setup stack
    mov ss, ax
    mov sp, 0x7C00              ; stack grows downwards from where we are loaded in memory

    ; read something from floppy disk
    ; BIOS should set DL to drive number
    mov [ebr_drive_number], dl

    mov ax, 1                   ; LBA=1, second sector from disk
    mov cl, 1                   ; 1 sector to read
    mov bx, 0x7E00              ; data should be after the bootloader
    call disk_read

    ; print welcome message
    mov si, msg_welcome
    call puts

    cli                         ; disable interrupts, this way CPU can't get out of "halt" state
    hlt


        
;
; Error handlers
;

floppy_error:
    mov si, msg_read_failed
    call puts
    jmp wait_key_and_reboot

wait_key_and_reboot:
    mov ah, 0
    int 16h                     ; wait for keypress
    jmp 0FFFFh:0                ; jump to beginning of BIOS, should reboot

.halt:
    cli                         ; disable interrupts, this way CPU can't get out of "halt" state
    hlt



;
; Disk routines
;

; -- Function
; Convert LBA address to CHS address
; Params:
;    - ax: LBA address
; Returns:
;    - cx [bits 0-5]: sector
;    - cx [bits 6-15]: cylinder
;    - dh: header
lba_to_chs:

    push ax
    push dx

    xor dx, dx                          ; dx = 0
    div word [bdb_sectors_per_track]    ; ax = LBA / SectorsPerTrack
                                        ; dx = LBA % SectorsPerTrack

    inc dx                              ; dx = (LBA % SectorsPerTrack + 1) = sector
    mov cx, dx                          ; cx = sector

    xor dx, dx                          ; dx = 0
    div word [bdb_heads]                ; ax = (LBA / SectorsPerTrack) / Heads = cylinder
                                        ; dx = (LBA / SectorsPerTrack) % Heads = head
    mov dh, dl                          ; dh = head
    mov ch, al                          ; ch = cylinder (lower 8 bits)
    shl ah, 6
    or cl, ah                           ; put upper 2 bits of cylinder in CL

    pop ax
    mov dl, al                          ; restore DL
    pop ax
    ret

; -- Function
; Reads sectors from a disk
; Params:
;   - ax: LBA address
;   - cl: number of sectors to read (up to 128)
;   - dl: drive number
;   - es:bx: memory address where to store read data
;
disk_read:

    push ax                             ; save registers we will modify
    push bx
    push cx
    push dx
    push di

    push cx                             ; temporarily save CL (number of sectors to read)
    call lba_to_chs                     ; compute CHS
    pop ax                              ; AL = number of sectors to read
    
    mov ah, 02h
    mov di, 3                           ; retry count

; docs say that floppy disk are unreliable and the interrupt should be tried several times
.retry:
    pusha                               ; save all registers, we don't know what bios modifies
    stc                                 ; set carry flag, some BIOS'es don't set it
    int 13h                             ; carry flag cleared = success
    jnc .done                           ; jump if carry not set

    ; read failed
    popa
    ; call disk_reset

    dec di
    test di, di
    jnz .retry

.fail:
    ; all attempts are exhausted
    jmp floppy_error

.done:
    popa

    pop di
    pop dx
    pop cx
    pop bx
    pop ax                             ; restore registers modified
    ret



; -- Function
; Resets disk controller
; Params:
;   dl: drive number
;
disk_reset:
    pusha
    mov ah, 0
    stc
    int 13h
    jc floppy_error
    popa
    ret



msg_welcome:            db 'Welcome to the custom OS', ENDL, 0
msg_read_failed:        db 'Read from disk failed!', ENDL, 0



; in legacy booting mode the BIOS expects that the last to bytes of the first sector are 0xAA55
; on sector has 512 bytes
; 510 - length of program -> used to pad the program to a size of 510
times 510-($-$$) db 0
; add signature to byte 511 & 512
dw 0AA55h

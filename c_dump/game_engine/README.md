# Game Engine

The cool of this code is to create a DOOM like game engine.


## Dependencies

- SDL2 (Simple DirectMedia Layer)
  - install sdl2: yay -S sdl2
  - create smylink to sdl2 lib inside libs/linux_sdl2
    - find binary: sdl2-config --libs
    - create symlink inside libs/linux_sdl2: ln -s /usr/lib .
  - create symlink to sdl2 includes inside libs/linux_sdl2/include
    - find include: sdl-config --cflags
    - create symlink inside libs/linux_sdl2/includes: ln -s /usr/include/SDL2 .

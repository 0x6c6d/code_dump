#include <stdbool.h>
#include <stdio.h>

bool predicate(int number) { return number % 2 == 0; }

void print_if(int numbers[10], bool (*predicate)(int)) {
  for (int i = 0; i < 10; ++i) {
    if (predicate(numbers[i])) {
      printf("%d\n", numbers[i]);
    }
  }
}

int main() {
  int numbers[] = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

  print_if(numbers, predicate);

  return 0;
}

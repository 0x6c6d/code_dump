#include <stdio.h>

struct fraction
{
    int num;
    int denum;
};

int main()
{
    struct fraction frac;
    frac.num = 10;
    frac.denum = 2;

    return 0;
}

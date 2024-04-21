#include <iostream>

class Animal
{
public:
    Animal(){};
    virtual ~Animal(){};
    virtual void Speak() const { std::cout << "Animal sound"; }
};

class Dog : public Animal
{
public:
    Dog(){};
    ~Dog(){};
    void Speak() const { std::cout << "Wuff"; }
};

class Cat : public Animal
{
public:
    Cat(){};
    ~Cat(){};
    void Speak() const { std::cout << "Meow"; }
};

int main()
{
    Animal *animal1 = new Dog();
    Animal *animal2 = new Cat();
    animal1->Speak();
    animal2->Speak();
    delete animal1;
    delete animal2;

    return 0;
}

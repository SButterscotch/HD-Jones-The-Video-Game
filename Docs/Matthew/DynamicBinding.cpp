#include <iostream>
#include <string>

class Animal {
public:
    virtual std::string make_sound() {
        return "Animal sound";
    }
};

class Dog : public Animal {
public:
    std::string make_sound() override {
        return "Woof!";
    }
};

class Cat : public Animal {
public:
    std::string make_sound() override {
        return "Meow!";
    }
};

// Static binding example
void static_binding_example(Animal* animal) {
    std::cout << "Static binding example:" << std::endl;
    std::cout << "Type of animal: " << typeid(*animal).name() << std::endl;
    std::cout << "Animal sound: " << animal->make_sound() << std::endl;
}

// Dynamic binding example
void dynamic_binding_example(Animal* animal) {
    std::cout << "Dynamic binding example:" << std::endl;
    std::cout << "Type of animal: " << typeid(*animal).name() << std::endl;
    std::cout << "Animal sound: " << animal->make_sound() << std::endl;
}

int main() {
    // Static binding example
    Dog static_dog;
    Cat static_cat;

    // Dynamic binding example
    Animal* dynamic_animal = nullptr;

    // Setting the dynamic type to Dog
    dynamic_animal = new Dog();

    // Now let's demonstrate static and dynamic binding
    static_binding_example(&static_dog);
    static_binding_example(&static_cat);

    dynamic_binding_example(dynamic_animal);

    delete dynamic_animal; // Don't forget to free memory
    return 0;
}


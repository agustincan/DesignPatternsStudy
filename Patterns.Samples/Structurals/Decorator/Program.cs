using Patterns.Samples.Decorator.Base;

namespace Patterns.Samples.Decorator;

class Program
{
    static void Main(string[] args)
    {
        // Crear un componente concreto
        IComponent component = new ConcreteComponent();
        Console.WriteLine(component.Operation()); // Output: ConcreteComponent

        // Decorar el componente con ConcreteDecoratorA
        IComponent decoratedComponentA = new ConcreteDecoratorA(component);
        Console.WriteLine(decoratedComponentA.Operation()); // Output: ConcreteDecoratorA(ConcreteComponent)

        // Decorar el componente con ConcreteDecoratorB
        IComponent decoratedComponentB = new ConcreteDecoratorB(decoratedComponentA);
        Console.WriteLine(decoratedComponentB.Operation()); // Output: ConcreteDecoratorB(ConcreteDecoratorA(ConcreteComponent))
    }
}
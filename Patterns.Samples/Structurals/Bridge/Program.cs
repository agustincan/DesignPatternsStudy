namespace Patterns.Samples.Bridge;

// El patrón de diseño Bridge (o Puente) es un patrón estructural que separa la abstracción de su implementación,
// permitiendo que ambas puedan variar independientemente. Este patrón es útil cuando se necesita
// desacoplar una abstracción de su implementación para que ambas puedan evolucionar por separado
// sin afectarse mutuamente.

class Program
{
    static void Main(string[] args)
    {
        // Crear una implementación concreta A
        IImplementation implementationA = new ConcreteImplementationA();
        var abstractionA = new Abstraction(implementationA);
        Console.WriteLine(abstractionA.Operation()); // Output: Abstraction: ConcreteImplementationA

        // Crear una implementación concreta B
        IImplementation implementationB = new ConcreteImplementationB();
        var refinedAbstractionB = new RefinedAbstraction(implementationB);
        Console.WriteLine(refinedAbstractionB.Operation()); // Output: RefinedAbstraction: ConcreteImplementationB
    }
}
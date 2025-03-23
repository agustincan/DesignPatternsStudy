namespace Patterns.Samples.Behaviors.ChainOfResponsability;

// El patrón de diseño Chain of Responsibility (o Cadena de Responsabilidad) es un patrón de comportamiento
// que permite que varios objetos tengan la oportunidad de procesar una solicitud. Estos objetos se encadenan
// y la solicitud pasa a través de la cadena hasta que un objeto la maneja o hasta que llega al final de la
// cadena sin ser procesada. Este patrón es útil cuando se necesita desacoplar el emisor de una solicitud de
// sus receptores, permitiendo que múltiples objetos tengan la oportunidad de manejar la solicitud.

class Program
{
    static void Main(string[] args)
    {
        // Configurar la cadena de responsabilidad
        var monkey = new MonkeyHandler();
        var squirrel = new SquirrelHandler();
        var dog = new DogHandler();

        monkey.SetNext(squirrel).SetNext(dog);

        // Probar la cadena con diferentes solicitudes
        Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
        Client.ClientCode(monkey);
        Console.WriteLine();

        Console.WriteLine("Subchain: Squirrel > Dog\n");
        Client.ClientCode(squirrel);
    }
}
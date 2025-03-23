using Patterns.Samples.Decorator.Base;

namespace Patterns.Samples.Decorator;

public class ConcreteComponent : IComponent
{
    public string Operation()
    {
        return "ConcreteComponent";
    }
}
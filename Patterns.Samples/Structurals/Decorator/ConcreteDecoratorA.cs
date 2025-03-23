using Patterns.Samples.Decorator.Base;

namespace Patterns.Samples.Decorator;

public class ConcreteDecoratorA : Base.Decorator
{
    public ConcreteDecoratorA(IComponent component) : base(component)
    {
    }

    public override string Operation()
    {
        return $"ConcreteDecoratorA({base.Operation()})";
    }
}
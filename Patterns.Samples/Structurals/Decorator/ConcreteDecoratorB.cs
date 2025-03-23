using Patterns.Samples.Decorator.Base;

namespace Patterns.Samples.Decorator;

public class ConcreteDecoratorB : Base.Decorator
{
    public ConcreteDecoratorB(IComponent component) : base(component)
    {
    }

    public override string Operation()
    {
        return $"ConcreteDecoratorB({base.Operation()})";
    }
}
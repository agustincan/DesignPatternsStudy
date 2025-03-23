namespace Patterns.Samples.Decorator.Base;

public abstract class Decorator : IComponent
{
    protected IComponent _component;

    public Decorator(IComponent component)
    {
        _component = component;
    }

    public virtual string Operation()
    {
        return _component.Operation();
    }
}
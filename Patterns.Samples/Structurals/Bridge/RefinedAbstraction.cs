namespace Patterns.Samples.Bridge;

public class RefinedAbstraction : Abstraction
{
    public RefinedAbstraction(IImplementation implementation) : base(implementation)
    {
    }

    public override string Operation()
    {
        return $"RefinedAbstraction: {_implementation.OperationImplementation()}";
    }
}
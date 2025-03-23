namespace Patterns.Samples.Behaviors.ChainOfResponsability;

public class MonkeyHandler : AbstractHandler
{
    public override object Handle(object request)
    {
        if ((request as string) == "Banana")
        {
            return $"Monkey: I'll eat the {request}.";
        }
        else
        {
            return base.Handle(request);
        }
    }
}
namespace Patterns.Samples.Behaviors.ChainOfResponsability;

public class DogHandler : AbstractHandler
{
    public override object Handle(object request)
    {
        if ((request as string) == "MeatBall")
        {
            return $"Dog: I'll eat the {request}.";
        }
        else
        {
            return base.Handle(request);
        }
    }
}
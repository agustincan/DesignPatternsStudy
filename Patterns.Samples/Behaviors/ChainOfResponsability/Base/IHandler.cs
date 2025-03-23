namespace Patterns.Samples.Behaviors.ChainOfResponsability.Base;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    object Handle(object request);
}
namespace Patterns.Samples.Outbox;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Destination { get; set; }
    public string Status { get; set; }
}
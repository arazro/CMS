using SharedKernel.Messaging.Abstractions;

namespace SharedKernel.Messaging.Events;

public abstract class BaseEvent : IEvent
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }

    protected BaseEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
}

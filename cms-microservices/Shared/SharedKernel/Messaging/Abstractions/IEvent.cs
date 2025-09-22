namespace SharedKernel.Messaging.Abstractions;

public interface IEvent
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}


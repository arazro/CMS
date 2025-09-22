namespace SharedKernel.Messaging.Abstractions;

public interface ICommand
{
     Guid Id { get; }
    DateTime OccurredOn { get; }
}

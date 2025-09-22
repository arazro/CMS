using SharedKernel.Messaging.Abstractions;

namespace SharedKernel.Messaging.Commands;

public abstract class BaseCommand : ICommand
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }

    protected BaseCommand()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
}

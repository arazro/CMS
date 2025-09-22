namespace SharedKernel.Messaging.Commands;

public class UpdateUserCommand : BaseCommand
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }

    public UpdateUserCommand(Guid userId, string email, string fullName)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
    }
}

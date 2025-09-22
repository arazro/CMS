namespace SharedKernel.Messaging.Events;

public class UserUpdatedEvent : BaseEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }

    public UserUpdatedEvent(Guid userId, string email, string fullName)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
    }
}

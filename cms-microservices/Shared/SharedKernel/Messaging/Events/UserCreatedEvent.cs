namespace SharedKernel.Messaging.Events;

public class UserCreatedEvent : BaseEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }

    public UserCreatedEvent(Guid userId, string email, string fullName)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
    }
}

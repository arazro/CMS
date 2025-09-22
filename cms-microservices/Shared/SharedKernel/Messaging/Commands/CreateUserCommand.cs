namespace SharedKernel.Messaging.Commands;

public class CreateUserCommand : BaseCommand
{
    public string Email { get; }
    public string FullName { get; }
    public string Password { get; }

    public CreateUserCommand(string email, string fullName, string password)
    {
        Email = email;
        FullName = fullName;
        Password = password;
    }
}

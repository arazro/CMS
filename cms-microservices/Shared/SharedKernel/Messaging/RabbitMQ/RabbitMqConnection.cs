using RabbitMQ.Client;

namespace SharedKernel.Messaging.RabbitMQ;

public class RabbitMqConnection : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public IModel Channel => _channel;

    public RabbitMqConnection(RabbitMqSettings settings)
    {
        var factory = new ConnectionFactory()
        {
            HostName = settings.HostName,
            Port = settings.Port,
            UserName = settings.UserName,
            Password = settings.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}

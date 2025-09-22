using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace SharedKernel.Messaging.RabbitMQ;

public class RabbitMqPublisher
{
    private readonly IModel _channel;

    public RabbitMqPublisher(RabbitMqConnection connection)
    {
        _channel = connection.Channel;
    }

    public void Publish<T>(string exchange, string routingKey, T message)
    {
        _channel.ExchangeDeclare(exchange, ExchangeType.Direct, durable: true);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        _channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
    }
}

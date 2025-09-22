using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SharedKernel.Messaging.RabbitMQ;

public class RabbitMqConsumer
{
    private readonly IModel _channel;

    public RabbitMqConsumer(RabbitMqConnection connection)
    {
        _channel = connection.Channel;
    }

    public void Subscribe<T>(string exchange, string queue, string routingKey, Action<T> handler)
    {
        _channel.ExchangeDeclare(exchange, ExchangeType.Direct, durable: true);
        _channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(queue, exchange, routingKey);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));
            if (message != null)
                handler(message);
        };

        _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
    }
}

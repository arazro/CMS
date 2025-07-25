using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Messaging
{
    public class UserCreatedPublisher
    {
        //private readonly IConnection _connection;
        //private RabbitMQ.Client.IModel? _channel;


        //public UserCreatedPublisher()
        //{
        //    var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        //    _connection = factory.CreateConnection();
        //    _channel = _connection.CreateModel();

        //    _channel.QueueDeclare(queue: "user-created-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        //}

        //public void Publish(User user)
        //{
        //    var message = JsonSerializer.Serialize(user);
        //    var body = Encoding.UTF8.GetBytes(message);

        //    _channel.BasicPublish(exchange: "", routingKey: "user-created-queue", basicProperties: null, body: body);
        //}
    }
}

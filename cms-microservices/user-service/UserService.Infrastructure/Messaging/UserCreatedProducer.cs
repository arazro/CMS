using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Messaging
{
    public class UserCreatedProducer
    {
        //private readonly IConnection _connection;
        //private readonly RabbitMQ.Client.IModel _channel;

        //public UserCreatedProducer()
        //{
        //    var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        //    _connection = factory.CreateConnection();
        //    _channel = _connection.CreateModel();

        //    _channel.QueueDeclare(
        //        queue: "user-created-queue",
        //        durable: true,
        //        exclusive: false,
        //        autoDelete: false,
        //        arguments: null
        //    );
        //}

        //public void PublishUserCreated(User user)
        //{
        //    var payload = new
        //    {
        //        user.Id,
        //        user.Username,
        //        user.Email
                
        //    };

        //    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload));

        //    _channel.BasicPublish(
        //        exchange: "",
        //        routingKey: "user-created-queue",
        //        basicProperties: null,
        //        body: body
        //    );

        //    Console.WriteLine($"[x] Sent UserCreated: {Encoding.UTF8.GetString(body)}");
        //}
    }
}

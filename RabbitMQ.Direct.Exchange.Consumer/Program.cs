using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://sycklyme:/sycklyme");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "Example", type: ExchangeType.Direct);
string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
 queue: queueName,
 exchange: "Example",
 routingKey: "Example"
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();


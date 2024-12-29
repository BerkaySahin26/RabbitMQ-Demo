using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://sycklyme-****");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "Example", exclusive: false);//birebir aynı tanımlamalar

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:"Example",false, consumer);
consumer.Received += (sender, e) =>
{

    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();
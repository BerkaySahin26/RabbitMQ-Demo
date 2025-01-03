using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://sycklyme:/sycklyme");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "Example", type: ExchangeType.Direct);
while (true)
{
    Console.Write("Mesaj : ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "Example",
        routingKey: "Example",
        body: byteMessage);
}

Console.Read();
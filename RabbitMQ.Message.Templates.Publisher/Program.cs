using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps:///sycklyme");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P (Point-to-Point) Tasarımı 
string queueName = "example-p2p-queue";

channel.QueueDeclare(
    queue:queueName,
    durable:false,
    exclusive:false,
    autoDelete:false
    );

byte[] message = Encoding.UTF8.GetBytes("merhaba");
channel.BasicPublish(
    exchange: string.Empty,
    routingKey: queueName,
    body:message
    );


#endregion

Console.Read();
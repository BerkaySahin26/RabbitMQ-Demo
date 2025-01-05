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
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: false,
    consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};


#endregion
#region Publish/Subscribe Tasarımı 

string exchangeName = "example-pub-sub-exchange";
channel.ExchangeDeclare(
    exchange: exchangeName,
    type: ExchangeType.Fanout
    );

string queueNames = channel.QueueDeclare().QueueName;
channel.QueueBind(
    queue: queueNames,
    exchange: exchangeName,
    routingKey:string.Empty
    );

EventingBasicConsumer consumer1 = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: false,
    consumer: consumer1
    );

consumer1.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

#endregion
Console.Read();
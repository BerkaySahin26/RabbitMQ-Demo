using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://sycklyme");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "Example", exclusive: false, durable:true);//birebir aynı tanımlamalar

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:"Example",autoAck:false, consumer);
channel.BasicQos(0, 1, false);
consumer.Received += (sender, e) =>
{

    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    channel.BasicAck(deliveryTag:e.DeliveryTag, multiple:false);  //bildirildi

};

Console.Read();
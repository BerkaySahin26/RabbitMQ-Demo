using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://sycklyme:/sycklyme");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//channel.QueueDeclare(queue: "Example", exclusive: false, durable:true);//birebir aynı tanımlamalar

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue:"Example",autoAck:false, consumer);
//channel.BasicQos(0, 1, false);
//consumer.Received += (sender, e) =>
//{

//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//    channel.BasicAck(deliveryTag:e.DeliveryTag, multiple:false);  //bildirildi

//};

//------------Direct----------- hedef kuyruk 


channel.ExchangeDeclare(exchange: "Example", type: ExchangeType.Direct);
string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
 queue: queueName,
 exchange: "Example",
 routingKey: "Example"
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue:queueName,
    autoAck:true,
    consumer:consumer
    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();
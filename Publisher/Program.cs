using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://sycklyme");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "Example", exclusive:false, durable:true );

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

//Byte 
//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange:"",routingKey:"Example",body: message);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba"+ i);
    channel.BasicPublish(exchange:"",routingKey:"Example",body: message, basicProperties:properties);
}

Console.Read();
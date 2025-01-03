﻿using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://sycklyme:/sycklyme");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//channel.QueueDeclare(queue: "Example", exclusive:false, durable:true );

//IBasicProperties properties = channel.CreateBasicProperties();
//properties.Persistent = true;

////Byte 
////byte[] message = Encoding.UTF8.GetBytes("Merhaba");
////channel.BasicPublish(exchange:"",routingKey:"Example",body: message);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);
//    byte[] message = Encoding.UTF8.GetBytes("Merhaba"+ i);
//    channel.BasicPublish(exchange:"",routingKey:"Example",body: message, basicProperties:properties);
//}

//-------Direct----------
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
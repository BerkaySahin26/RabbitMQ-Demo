using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;
string rabbitMQUri = "";

string queueName = "example-queue";
IBusControl bus = Bus.Factory.CreateUsingRabbitMq
    (factory =>
    {
        factory.Host(rabbitMQUri);
    });

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.Write("Gönderlicek mesaj :");
string message = Console.ReadLine();
await sendEndpoint.Send<IMessage>(new ExampleMessage()
{
    Text = message 
});

Console.Read();
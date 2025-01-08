using MassTransit;
using RabbitMQ.ESB.MassTransit.RequestResponse.Consumer.Consumer;


Console.WriteLine("Consumer");

string rabbitMQUri = "";

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(requestQueue, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();
using RabbitMQ.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    internal class Program
    {
        static void Main(string[] args)
        {

            

            ConnectionFactory factory = new();
            factory.Uri = new("amqps://sycklyme:5lcV1RjbgCE-ruogZintqOgyaL-Lf01W@rat.rmq2.cloudamqp.com/sycklyme");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDecleare(queue)

        }
    }
}

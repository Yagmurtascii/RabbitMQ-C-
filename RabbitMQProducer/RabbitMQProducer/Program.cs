// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
namespace RabbitMQProducer
{
    static class Program 
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localHost"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("queue", exclusive: false, autoDelete: false);
            var messages = new { Message = "Hello, RabbitMQ" };
            var json = JsonConvert.SerializeObject(messages);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(body:body,routingKey:"queue",exchange:"");
        }
    }
}


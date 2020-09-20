using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient.RabbitMQTest
{
    public class RabbitMQProg
    {
        public static void Run()
        {
            string UserName = "guest";
            string Password = "guest";
            string HostName = "localhost";

            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = UserName,
                Password = Password,
                HostName = HostName
            };
            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            //model.ExchangeDeclare("demoExchange", ExchangeType.Direct);

            //model.QueueDeclare("demoqueue", true, false, false, null);
            //Console.WriteLine("Creating Queue");

            //model.QueueBind("demoqueue", "demoExchange", "directexchange_key");
            //Console.WriteLine("Creating Binding");

            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            var messageBuffer = Encoding.Default.GetBytes("Direct Message");
            model.BasicPublish("demoExchange", "directexchange_key", properties, messageBuffer);
            Console.WriteLine("Message Sent");

            Console.ReadLine();
        }
    }
}

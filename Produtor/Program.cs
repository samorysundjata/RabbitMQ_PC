using RabbitMQ.Client;
using System;
using System.Text;

namespace Produtor
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "filaMQ",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int count = 0;
                while (true)
                {
                    string message = $"{count++} Salve a todos!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "filaMQ",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    System.Threading.Thread.Sleep(200);
                }
            }
        }

        #endregion Private Methods
    }
}
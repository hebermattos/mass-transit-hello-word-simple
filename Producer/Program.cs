using System;
using System.Threading;
using MassTransit;
using messages;
using Newtonsoft.Json;

namespace producer
{
    public class Program
    {
        public static void Main()
        {
            var _bus = ConnectQueue();

            Console.WriteLine("Producer connected!");

            var newMessge = new Message
            {
                Key = "key",
                Value = "value"
            };

            _bus.Publish(newMessge);

            Console.WriteLine($"sending: { JsonConvert.SerializeObject(newMessge) }");
        }

        private static IBusControl ConnectQueue()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            bus.Start();

            return bus;
        }
    }
}
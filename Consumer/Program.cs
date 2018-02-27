using System;
using System.Threading;
using messages;
using MassTransit;
using StackExchange.Redis;

namespace consumer
{
    public class Program
    {
        private static ManualResetEvent _handler = new ManualResetEvent(false);

        public static void Main()
        {
            var connected = false;

            while (!connected)
            {
                try
                {
                    ConnectQueue();
                    connected = true;
                    Console.WriteLine("Consumer connected!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro on consumer connection: {ex.Message}");
                }
            }

            _handler.WaitOne();

        }

        private static void ConnectQueue()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://queue"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "message_queue", ep =>
                {
                    ep.Consumer<MessageProcessor>();
                });
            });

            bus.Start();
        }
    }
}
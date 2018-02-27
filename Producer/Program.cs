using System;
using System.Threading;
using MassTransit;
using messages;
using Newtonsoft.Json;

namespace producer
{

    public class Program
    {
        private static IBusControl _bus;

        public static void Main()
        {
            var connected = false;

            while (!connected)
            {
                try
                {
                    _bus = ConnectQueue();
                    connected = true;
                    Console.WriteLine("Producer connected!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro on producer connection: {ex.Message}");
                }
            }

            var key = 1;

            while (true)
            {
                var newMessge = new Message
                {
                    Key = key.ToString(),
                    Value = "message_" + key
                };

                _bus.Publish(newMessge);

                Console.WriteLine($"enviando: { JsonConvert.SerializeObject(newMessge) }");

                key++;

                Thread.Sleep(5000);
            }
        }

        private static IBusControl ConnectQueue()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://queue"), h =>
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
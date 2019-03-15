using System;
using System.Threading;
using messages;
using MassTransit;
using GreenPipes;

namespace consumer
{
    public class Program
    {
        private static ManualResetEvent _handler = new ManualResetEvent(false);

        public static void Main()
        {
            ConnectQueue();

            Console.WriteLine("Consumer connected!");

            _handler.WaitOne();
        }

        private static void ConnectQueue()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "message_queue", ep =>
                {
                    ep.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });
                    ep.UseRetry(r => r.Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)));
                    ep.UseRateLimit(10, TimeSpan.FromSeconds(1));

                    ep.Consumer<MessageProcessor>();
                });
            });

            bus.Start();
        }
    }
}
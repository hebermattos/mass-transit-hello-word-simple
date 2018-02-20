using System;
using System.Threading;
using MassTransit;
using messages;

public class Program
{
    public static void Main()
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
        {
            var host = sbc.Host(new Uri("rabbitmq://rabbit"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        });

        bus.Start();

        for (int i = 0; i < Int16.MaxValue; i++)
        {
            bus.Publish(new YourMessage { Text = $"Hi number {i}" });

            Thread.Sleep(1000);
        }

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();

        bus.Stop();
    }
}
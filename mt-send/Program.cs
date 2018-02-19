using System;
using MassTransit;
using messages;

public class Program
{
    public static void Main()
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
        {
            var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("bill");
                h.Password("123qwe!@#QWE");
            });
        });

        bus.Start();

        bus.Publish(new YourMessage{Text = "Hi"});

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();

        bus.Stop();
    }
}
using System;
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

            sbc.ReceiveEndpoint(host, "test_queue", ep =>
            {
                ep.Handler<YourMessage>(context =>
                {
                    return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                });
            });
        });

        bus.Start();

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();

        bus.Stop();
        Console.WriteLine("Bus stopped");
    }
}
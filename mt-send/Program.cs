using System;
using System.Threading;
using MassTransit;
using messages;

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
                Console.WriteLine("Connected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro on connection: {ex.Message}");
            }
        }

        for (int i = 0; i < 100; i++)
        {
            _bus.Publish(new YourMessage { Text = $"Hi number {i}" });

            Thread.Sleep(1000);
        }

        _bus.Stop();
    }

    private static IBusControl ConnectQueue()
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
        
        return bus;
    }
}
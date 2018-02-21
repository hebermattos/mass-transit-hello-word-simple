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
                ConnectQueue();
                connected = true;
                Console.WriteLine("Connected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro on connection: {ex.Message}");
            }
        }

        while (true)
        {
            var data = new Random().Next(1000);

            _bus.Publish(new YourMessage { Text = data.ToString() });
            Console.WriteLine($"enviando: {data}");

            Thread.Sleep(1000);
        }
    }

    private static void ConnectQueue()
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
    }
}
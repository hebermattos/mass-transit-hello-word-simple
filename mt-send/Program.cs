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

        var key = 1;

        while (true)
        {
            var data = "message_" + key;

            _bus.Publish(new Message { Key = key.ToString(), Value = data });
            Console.WriteLine($"enviando: {data}");

            Thread.Sleep(1000);
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
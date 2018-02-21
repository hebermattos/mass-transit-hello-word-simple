using System;
using System.Threading;
using MassTransit;
using messages;

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
                Console.WriteLine("Connected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro on connection: {ex.Message}");
            }
        }

        _handler.WaitOne();

    }

    private static void ConnectQueue()
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
        {
            var host = sbc.Host(new Uri("rabbitmq://fila"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            sbc.ReceiveEndpoint(host, "message_queue", ep =>
            {
                ep.Handler<YourMessage>(context =>
                {
                    return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                });
            });
        });

        bus.Start();
    }
}
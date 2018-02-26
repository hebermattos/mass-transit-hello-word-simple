using System;
using System.Threading.Tasks;
using MassTransit;
using messages;
using StackExchange.Redis;

namespace messages
{
    public class MessageProcessor : IConsumer<Message>
    {
        public Task Consume(ConsumeContext<Message> context)
        {
            try
            {
                using (var redis = ConnectionMultiplexer.Connect("redis"))
                {
                    var db = redis.GetDatabase();

                    if (!db.StringSet(context.Message.Key, context.Message.Value))
                        throw new Exception("Message not saved on redis :(");
                }

                return Console.Out.WriteLineAsync("Message processed");
            }
            catch (Exception ex)
            {
                return Console.Out.WriteLineAsync($"Erro on message processing: {ex.Message}");
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using messages;
using Newtonsoft.Json;

namespace messages
{
    public class MessageProcessor : IConsumer<Message>
    {
        public Task Consume(ConsumeContext<Message> context)
        {
            Console.WriteLine("receive at: " + DateTime.Now.ToLongTimeString());

            throw new Exception("foo");

            return Console.Out.WriteLineAsync($"receiving: { JsonConvert.SerializeObject(context.Message) }");
        }
    }
}
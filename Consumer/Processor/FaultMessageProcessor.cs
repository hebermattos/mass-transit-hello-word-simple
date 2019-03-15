using System;
using System.Threading.Tasks;
using MassTransit;
using messages;
using Newtonsoft.Json;

namespace messages
{
    public class FaultMessageProcessor : IConsumer<Fault<Message>>
    {
        public Task Consume(ConsumeContext<Fault<Message>> context)
        {
           return Console.Out.WriteLineAsync("FAULT!!!!!!!!");
        }
    }
}
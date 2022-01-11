using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middt.Kafka.Common
{
    public class Consumer<T>
    {
        public Action<T>? OnConsume { get; set; }

        readonly string? _host;
        readonly int _port;
        readonly string? _topic;

        public Consumer()
        {
            _host = "localhost";
            _port = 9092;
             // _topic = "midd_topic";
             _topic = "midd_topic_target";
        }

        ConsumerConfig GetConsumerConfig()
        {
            return new ConsumerConfig
            {
                BootstrapServers = $"{_host}:{_port}",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Latest
                
                
            };
        }

        public async Task ConsumeAsync()
        {



            using (var consumer = new ConsumerBuilder<Null, T>(GetConsumerConfig())
                .SetValueDeserializer(new JsonValueDeserializer<T>())
                .Build())
            {
                consumer.Subscribe(_topic);

                Console.WriteLine($"Subscribed to {_topic}");


                await Task.Run(() =>
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume(default(CancellationToken));

                        if (consumeResult.Message is Message<Null, T> result)
                        {
                            OnConsume?.Invoke(result.Value);
                           
                        }
                    }
                });

                consumer.Close();
            }
        }
    }
}
﻿using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middt.Kafka.Common
{
    public class Producer<T>
    {

        readonly string? _host;
        readonly int _port;
        readonly string? _topic;

        public Producer()
        {
            _host = "localhost";
            _port = 9092;
            _topic = "midd_topic";
        }

        ProducerConfig GetProducerConfig()
        {
            return new ProducerConfig
            {
                BootstrapServers = $"{_host}:{_port}"
            };
        }

        public async Task ProduceAsync(T data)
        {
            using (var producer = new ProducerBuilder<Null, T>(GetProducerConfig())
                                                 .SetValueSerializer(new JsonValueSerializer<T>())
                                                 .Build())
            {
                await producer.ProduceAsync(_topic, new Message<Null, T> { Value = data });
                Console.WriteLine($"{data} published");
            }
        }
    }
}
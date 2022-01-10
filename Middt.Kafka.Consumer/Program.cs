using Confluent.Kafka;
using Middt.Kafka.Common;
using System.Net;

var consumer = new Consumer<TemperatureModel>();
consumer.OnConsume += ((TemperatureModel model) =>
{
    Console.WriteLine($"Data Received - {model.ToString()}");
});
await consumer.ConsumeAsync();

//var conf = new ConsumerConfig
//{
//    ClientId = Dns.GetHostName(),
//    GroupId = "test-consumer-group",
//    BootstrapServers = "localhost:9092",
//    AutoOffsetReset = AutoOffsetReset.Earliest
//};

//using (var c = new ConsumerBuilder<Ignore, TemperatureModel>(conf).SetValueDeserializer(new MiddtKafkaDeserializer<TemperatureModel>()).Build())
//{
//    c.Subscribe("test-topic");

//    CancellationTokenSource cts = new CancellationTokenSource();
//    Console.CancelKeyPress += (_, e) => {
//        e.Cancel = true; // prevent the process from terminating.
//        cts.Cancel();
//    };

//    try
//    {
//        while (true)
//        {
//            try
//            {
//                var cr = c.Consume(cts.Token);
//                Console.WriteLine($"Consumed message '{cr.Message.Value.ToString()}' at: '{cr.TopicPartitionOffset}'.");
//            }
//            catch (ConsumeException e)
//            {
//                Console.WriteLine($"Error occured: {e.Error.Reason}");
//            }
//        }
//    }
//    catch (OperationCanceledException)
//    {
//        // Ensure the consumer leaves the group cleanly and final offsets are committed.
//        c.Close();
//    }
//}
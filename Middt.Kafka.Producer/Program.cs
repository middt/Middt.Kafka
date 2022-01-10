using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Middt.Kafka.Common;
using System.Net;

var config = new ProducerConfig
{
    ClientId = Dns.GetHostName(),
    BootstrapServers = "localhost:9092"
};

var producer = new Producer<TemperatureModel>();
TemperatureModel model;

foreach (DateTime date in EachDay(DateTime.Today, DateTime.Today.AddDays(60)))
{
    model = Generate(date);
    await producer.ProduceAsync(model);

    await Task.Delay(1000);
}


Console.WriteLine("Publish Success!");



//using (var p = new ProducerBuilder<Null, TemperatureModel>(config)
//    .SetValueSerializer(new MiddtKafkaSerializer<TemperatureModel>()).Build())
//{
//    while (true)
//    {
//        Console.WriteLine("Please press enter to send a message");
//        Console.ReadLine();

//        TemperatureModel model;
//        foreach (DateTime date in EachDay(DateTime.Today, DateTime.Today.AddDays(60)))
//        {
//            try
//            {
//                model = Generate(date);
//                var t = p.ProduceAsync(Settings.Topic, new Message<Null, TemperatureModel>  {  Value = model });
//                t.ContinueWith(task =>
//                {
//                    if (task.IsFaulted)
//                    {
//                        Console.WriteLine($"Delivery failed");
//                    }
//                    else
//                    {
//                        Console.WriteLine($"Delivered '{task.Result.Value}' to '{task.Result.TopicPartitionOffset}'");
//                    }
//                });
//            }
//            catch (ProduceException<Null, string> e)
//            {
//                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
//            }
//        }
//    }
//}

TemperatureModel Generate(DateTime date)
{
    TemperatureModel model = new TemperatureModel();
    model.Date = date;
    model.HighTemp = new Random().Next(40, 50);
    model.LowTemp = new Random().Next(10, 20);
    model.Mean = (model.HighTemp + model.LowTemp) / 2;

    return model;
}
IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
{
    for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
        yield return day;
}

Console.ReadLine();

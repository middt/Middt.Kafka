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

//foreach (DateTime date in EachDay(DateTime.Today, DateTime.Today.AddDays(60)))
//{
//    model = Generate(date.);
//    await producer.ProduceAsync(model);

//    await Task.Delay(1000);
//}

int startIndex = 0;
int endIndex = 0;
while (true)
{
    endIndex = startIndex + 100;

    for (int i = startIndex; i < endIndex; i++)
    {
        model = Generate(i);
        await producer.ProduceAsync(model);

        await Task.Delay(1000);
    }

    Console.WriteLine("Publish Success!");

    startIndex = endIndex;
}


TemperatureModel Generate(int index)
{
    TemperatureModel model = new TemperatureModel();
    model.Index = index;
    model.HighTemp = new Random().Next(40, 50);
    model.LowTemp = new Random().Next(10, 20);
    model.Mean = (model.HighTemp + model.LowTemp) / 2;

    return model;
}
//IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
//{
//    for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
//        yield return day;
//}

Console.ReadLine();

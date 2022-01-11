using Confluent.Kafka;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Middt.Kafka.Common
{
    [Serializable]
    public record TemperatureModel
    { 
        // public DateTime Date { get; set; }
        public int Index { get; set; }
        public double HighTemp { get; set; }

        public double LowTemp { get; set; }

        public double Mean { get; set; }


        public override string ToString()
        {
            return $"Index: {Index} Temp: {LowTemp}-{HighTemp} Mean: {Mean}";
        }
    }
}
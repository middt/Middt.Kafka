using Confluent.Kafka;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Middt.Kafka.Common
{
    [Serializable]
    public record TemperatureModel
    { 
        public DateTime Date { get; set; }

        public double HighTemp { get; set; }

        public double LowTemp { get; set; }

        public double Mean { get; set; }


        public override string ToString()
        {
            return $"Date: {Date} Temp: {LowTemp}-{HighTemp} Mean: {Mean}";
        }
    }



}
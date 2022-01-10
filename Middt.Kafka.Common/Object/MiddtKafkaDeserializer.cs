using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Middt.Kafka.Common
{
    public class MiddtKafkaDeserializer<TModel> : IDeserializer<TModel>
    {
        public TModel Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (data == null)
                return default(TModel);

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data.ToArray()))
            {
                object obj = bf.Deserialize(ms);
                return (TModel)obj;
            }
        }
    }
}

using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Middt.Kafka.Common
{
    public class MiddtKafkaSerializer<TModel> : ISerializer<TModel>
    {
        public byte[] Serialize(TModel data, SerializationContext context)
        {
            if (data == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, data);
                return ms.ToArray();
            }
        }
    }
}

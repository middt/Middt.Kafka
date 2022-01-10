using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Middt.Kafka.Common
{
    public class JsonValueSerializer<TModel> : ISerializer<TModel>
    {
        public byte[] Serialize(TModel data, SerializationContext context)
        {
            if (data == null)
                return null;

            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, typeof(TModel)));
        }
    }
}

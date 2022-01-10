using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Middt.Kafka.Common
{
    public class JsonValueDeserializer<TModel> : IDeserializer<TModel>
    {
        public TModel Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (data == null)
                return default(TModel);

            return JsonSerializer.Deserialize<TModel>(data);
        }
    }
}
using System;
using System.Linq;
using Baymax.Extension;
using Baymax.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Baymax.Tests.Util
{
    public class EnumerationJsonCovert : JsonConverter
    {
        private readonly Type[] _types;

        public EnumerationJsonCovert()
        {
        }

        public EnumerationJsonCovert(params Type[] types)
        {
            _types = types;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken.FromObject(value).WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return string.Empty;
            }

            var jObject = JObject.Load(reader);

            var paramTypes = new[] { typeof(int), typeof(string) };
            var paramValues = new object[] { jObject["Value"].ToInt(), jObject["DisplayName"].ToString() };

            return Reflection.Construct(objectType, paramTypes, paramValues);
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(a => a == objectType);
        }
    }
}
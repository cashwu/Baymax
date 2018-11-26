using System;
using System.Linq;
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
            var propNames = jObject.Properties().Select(a => a.Name);

            var obj = Activator.CreateInstance(objectType);

            foreach (var name in propNames)
            {
                var propertyInfo = objectType.GetProperty(name);
                propertyInfo.SetValue(obj, Convert.ChangeType(jObject[name], propertyInfo.PropertyType));
            }

            return obj;
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(a => a == objectType);
        }
    }
}
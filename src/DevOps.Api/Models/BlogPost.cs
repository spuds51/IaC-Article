using System;
using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DevOps.Api.Models
{
    public class BlogPost
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Article { get; set; }
        
        [DynamoDBProperty("PostedDate")]
        [JsonConverter(typeof(JsonDateTimeConvertor))]
        public DateTime PostedDate { get; set; }
    }
    
    public class JsonDateTimeConvertor : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value?.ToString()!);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue( ((DateTime)value).ToString("yyyy-MM-dd") );
        }
    }
}
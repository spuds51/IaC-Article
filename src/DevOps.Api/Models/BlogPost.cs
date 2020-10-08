using System;
using System.Globalization;
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
        
        public string PostedDate { get; set; }
    }
    
    public class JsonDateTimeConvertor : DateTimeConverterBase
    {
        private const string Format = "yyyy-MM-dd";
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.ParseExact(reader.Value?.ToString()!, Format, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue( ((DateTime)value).ToString(Format) );
        }
    }
}
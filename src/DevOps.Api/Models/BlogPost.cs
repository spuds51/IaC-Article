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
        
        [DynamoDBProperty("TaskStatusLastUpdate")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime PostedDate { get; set; }
    }
}
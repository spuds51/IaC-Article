using System;

namespace DevOps.Api.Models
{
    public class BlogPost
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Article { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
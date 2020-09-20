using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Models.Blogs
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public List<string> Links { get; set; }
    }
}

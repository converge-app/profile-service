using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models.Entities
{
    public class Profile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Title { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public List<string> Experience { get; set; } = new List<string>();

    }
}
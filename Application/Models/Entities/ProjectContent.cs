using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models.Entities
{
    public class BiddingContent
    {
        [BsonRequired]
        public string Title { get; set; }

        [BsonRequired]
        public string Description { get; set; }

        [BsonRequired]
        public string Category { get; set; }

        [BsonRequired]
        public string SubCategory { get; set; }

        [BsonRequired]
        public decimal Amount { get; set; }
        public List<string> Files { get; set; } = new List<string>();
    }
}
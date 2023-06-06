using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient.Models
{
    public class Rating
    {
        [BsonElement("_id")]
        public ObjectId? Id { get; set; }

        [BsonElement("tconst")]
        public string Tconst { get; set; }

        [BsonElement("averageRating")]
        public double? AverageRating { get; set; }

        [BsonElement("numVotes")]
        public int? NumVotes { get; set; }
    }
}

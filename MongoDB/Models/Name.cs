using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient.Models
{
    public class Name
    {
        [BsonElement("_id")]
        public ObjectId? Id { get; set; }

        [BsonElement("nconst")]
        public string Tconst { get; set; }

        [BsonElement("primaryName")]
        public string PrimaryName { get; set; }

        [BsonElement("birthYear")]
        public int? BirthYear { get; set; }

        [BsonElement("deathYear")]
        public int? DeathYear { get; set; }

        [BsonElement("primaryProfession")]
        public string PrimaryProfession { get; set; }

        [BsonElement("knownForTitles")]
        public string KnownForTitles { get; set; }
    }
}

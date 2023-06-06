using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient.Models
{
    public class Title
    {
        [BsonElement("_id")]
        public ObjectId? Id { get; set; }

        [BsonElement("tconst")]
        public string Tconst { get; set; }

        [BsonElement("titleType")]
        public string TitleType { get; set; }

        [BsonElement("primaryTitle")]
        public string PrimaryTitle { get; set; }

        [BsonElement("originalTitle")]
        public string OriginalTitle { get; set; }

        [BsonElement("isAdult")]
        public int? IsAdult { get; set; }

        [BsonElement("startYear")]
        public int? StartYear { get; set; }

        [BsonElement("endYear")]
        public int? EndYear { get; set; }

        [BsonElement("runtimeMinutes")]
        public int? RuntimeMinutes { get; set; }

        [BsonElement("genres")]
        public string Genres { get; set; }
    }
}

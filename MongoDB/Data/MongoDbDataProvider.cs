using DBClient.Models;

using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient.Data
{
    /// <summary>
    /// Dostawca danych bazy IMDB MongoDb.
    /// </summary>
    public class MongoDbDataProvider : IMongoDbDataProvider
    {
        private readonly IMongoDatabase db;

        public MongoDbDataProvider(IMongoDatabase db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
        public IMongoCollection<BsonDocument> GetNameCollection()
        {
            return db.GetCollection<BsonDocument>("Name");
        }

        /// <inheritdoc/>
        public IMongoCollection<BsonDocument> GetRatingCollection()
        {
            return db.GetCollection<BsonDocument>("Rating");
        }

        /// <inheritdoc/>
        public IMongoCollection<BsonDocument> GetTitleCollection()
        {
            return db.GetCollection<BsonDocument>("Title");
        }
    }
}

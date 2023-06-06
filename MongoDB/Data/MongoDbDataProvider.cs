using DBClient.Models;

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
        public IMongoCollection<Name> GetNameCollection()
        {
            return db.GetCollection<Name>("Name");
        }

        /// <inheritdoc/>
        public IMongoCollection<Rating> GetRatingCollection()
        {
            return db.GetCollection<Rating>("Rating");
        }

        /// <inheritdoc/>
        public IMongoCollection<Title> GetTitleCollection()
        {
            return db.GetCollection<Title>("Title");
        }
    }
}

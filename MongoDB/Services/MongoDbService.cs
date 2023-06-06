using DBClient.Data;
using DBClient.Models;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient.Services
{
    /// <summary>
    /// Serwis do wyznaczania informacji z bazy danych IMDB MongoDb
    /// </summary>
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoDbDataProvider db;

        public MongoDbService(IMongoDbDataProvider db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
        public void Exercise1()
        {
            var ratingCollection = db.GetRatingCollection();
            var nameCollection = db.GetNameCollection();
            var titleCollection = db.GetTitleCollection();

            var ratingCount = ratingCollection.Find(Builders<Rating>.Filter.Empty).CountDocuments();
            var nameCount = nameCollection.Find(Builders<Name>.Filter.Empty).CountDocuments();
            var titleCount = titleCollection.Find(Builders<Title>.Filter.Empty).CountDocuments();

            string result = $"Rating: {ratingCount}, Name: {nameCount}, Title: {titleCount}";
            Console.WriteLine(result);
        }
    }
}

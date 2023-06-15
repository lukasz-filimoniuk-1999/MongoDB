using DBClient.Data;
using DBClient.Models;

using MongoDB.Bson;
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

            var ratingCount = ratingCollection.Find(Builders<BsonDocument>.Filter.Empty).CountDocuments();
            var nameCount = nameCollection.Find(Builders<BsonDocument>.Filter.Empty).CountDocuments();
            var titleCount = titleCollection.Find(Builders<BsonDocument>.Filter.Empty).CountDocuments();

            string result = $"Rating: {ratingCount}, Name: {nameCount}, Title: {titleCount}";
            Console.WriteLine(result);
        }

        public void Exercise2()
        {
            var titleCollection = db.GetTitleCollection();

            var limitedResult = titleCollection.Find(Builders<BsonDocument>.Filter.And(
                          Builders<BsonDocument>.Filter.Eq("startYear", 2010),
                          Builders<BsonDocument>.Filter.Regex("genres", new BsonRegularExpression("Romance")),
                          Builders<BsonDocument>.Filter.Gt("runtimeMinutes", 90),
                          Builders<BsonDocument>.Filter.Lte("runtimeMinutes", 120)))
                .Project(Builders<BsonDocument>.Projection.Include("primaryTitle").Include("startYear").Include("genres").Include("runtimeMinutes").Exclude("_id"))
                .Sort(Builders<BsonDocument>.Sort.Ascending("primaryTitle"))
                .Limit(5)
                .ToList();

            limitedResult.ForEach(x => Console.WriteLine(x));
        }

        public void Exercise3()
        {
            var titleCollection = db.GetTitleCollection();

            var limitedResult = titleCollection.Find(Builders<BsonDocument>.Filter.And(
                          Builders<BsonDocument>.Filter.Eq("startYear", 2010),
                          Builders<BsonDocument>.Filter.Regex("genres", new BsonRegularExpression("Romance")),
                          Builders<BsonDocument>.Filter.Gt("runtimeMinutes", 90),
                          Builders<BsonDocument>.Filter.Lte("runtimeMinutes", 120)))
                .Project(Builders<BsonDocument>.Projection.Include("primaryTitle").Include("startYear").Include("genres").Include("runtimeMinutes").Exclude("_id"))
                .Sort(Builders<BsonDocument>.Sort.Ascending("primaryTitle"))
                .Limit(5)
                .ToList();

            limitedResult.ForEach(x => Console.WriteLine(x));
        }
    }
}

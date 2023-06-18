using DBClient.Data;

using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Linq;

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
            Console.WriteLine("Zadanie 1#");
            var ratingCollection = db.GetRatingCollection();
            var nameCollection = db.GetNameCollection();
            var titleCollection = db.GetTitleCollection();

            var ratingCount = ratingCollection.Find(Builders<BsonDocument>.Filter.Empty).CountDocuments();
            var nameCount = nameCollection.Find(Builders<BsonDocument>.Filter.Empty).CountDocuments();
            var titleCount = titleCollection.Find(Builders<BsonDocument>.Filter.Empty).CountDocuments();

            string result = $"Rating: {ratingCount}, Name: {nameCount}, Title: {titleCount}";
            Console.WriteLine(result);
            Console.WriteLine("\n");
        }

        public void Exercise2()
        {
            Console.WriteLine("Zadanie 2#");
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
            Console.WriteLine("\n");
        }

        public void Exercise3()
        {
            Console.WriteLine("Zadanie 3#");
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
            Console.WriteLine("\n");
        }

        public void Exercise4()
        {
            Console.WriteLine("Zadanie 4#");
            var titleCollection = db.GetTitleCollection();
            var ratingCollection = db.GetRatingCollection();

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.In("startYear", new[] { 1999, 2000}),
                Builders<BsonDocument>.Filter.Regex("genres", new BsonRegularExpression("Documentary")));

            var documentCount = titleCollection.CountDocuments(filter);

            Console.WriteLine("Liczba dokumentów spełniających warunki: " + documentCount + "\n");

            var limitedCollection = titleCollection.Aggregate().Match(Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.In("startYear", new[] { 1999, 2000 }),
                Builders<BsonDocument>.Filter.Regex("genres", new BsonRegularExpression("Documentary"))))
                .Lookup("Rating", "tconst", "tconst", "avg_rating")
                .Project(Builders<BsonDocument>.Projection.Include("primaryTitle").Include("startYear").Include("avg_rating.averageRating").Exclude("_id"))
                .Sort(Builders<BsonDocument>.Sort.Descending("avg_rating.averageRating"))
                .Limit(5)
                .ToList();

            limitedCollection.ForEach(d => Console.WriteLine(d));
            Console.WriteLine("\n");
        }

        public void Exercise5()
        {
            Console.WriteLine("Zadanie 5#");
            var nameCollection = db.GetNameCollection();
            var textIndex = Builders<BsonDocument>.IndexKeys.Text("primaryName");
            var indexModel = new CreateIndexModel<BsonDocument>(textIndex);

            nameCollection.Indexes.CreateOne(indexModel);

            var filter = Builders<BsonDocument>.Filter.Text("Fonda Coppola", new TextSearchOptions { CaseSensitive = true});

            var countDocuments = nameCollection.CountDocuments(filter);

            Console.WriteLine("Liczba dokumentów spełniających wymagania: " + countDocuments);

            var projection = Builders<BsonDocument>.Projection
                .Include("primaryName")
                .Include("primaryProfession")
                .Exclude("_id");

            var result = nameCollection.Find(filter).Project(projection).Limit(5).ToList();
            result.ForEach(d => Console.WriteLine(d));
            Console.WriteLine("\n");
        }

        public void Exercise6()
        {
            Console.WriteLine("Zadanie 6#");
            var nameCollection = db.GetNameCollection();
            var indexKeysDefinition = Builders<BsonDocument>.IndexKeys.Descending("birthYear");
            var indexModel = new CreateIndexModel<BsonDocument>(indexKeysDefinition);

            nameCollection.Indexes.CreateOne(indexModel);

            var indexes = nameCollection.Indexes.List().ToList();

            Console.WriteLine("Lista indeksów w kolekcji Name:");
            foreach (var index in indexes)
            {
                Console.WriteLine(index["name"]);
            }

            Console.WriteLine($"Liczba indeksów w kolekcji Name: {indexes.Count()}");

        }

        public void Exercise7()
        {
            var titleCollection = db.GetTitleCollection();
            Console.WriteLine("Zadanie 7#");
            var pipeline = new[]
            {
                // Grupowanie filmów według tytułu i obliczanie maksymalnej średniej oceny
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$Title" },
                    { "maxAvgRating", new BsonDocument("$max", "$Rating.averageRating") }
                }),
                // Wybieranie tylko filmów z maksymalną średnią oceną
                new BsonDocument("$match", new BsonDocument
                {
                    { "maxAvgRating", 10.0 }
                }),
                // Dodawanie pola "max" o wartości równiej 1
                new BsonDocument("$addFields", new BsonDocument
                {
                    { "max", 1 }
                })
            };

            var result = titleCollection.Aggregate<BsonDocument>(pipeline).ToList();
            var updateCountDocuments = result.Count();

            Console.WriteLine(updateCountDocuments);
        }

        public void Exercise8()
        {
            Console.WriteLine("Zadanie 8#");
            var titleCollection = db.GetTitleCollection();

            var titleInfoDocument = titleCollection.Aggregate().Match(Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("primaryTitle", "The Derby 1895"),
                Builders<BsonDocument>.Filter.Eq("startYear", 1895)))
                .Lookup("Rating", "tconst", "tconst", "avg_rating")
                .Project(Builders<BsonDocument>.Projection.Include("primaryTitle").Include("startYear").Include("avg_rating.averageRating").Exclude("_id"))
                .ToList();

            titleInfoDocument.ForEach(d => Console.WriteLine(d));
            Console.WriteLine("\n");
        }

        public void Exercise9()
        {
            Console.WriteLine("Zadanie 9#");
            var titleCollection = db.GetTitleCollection();
            var ratingCollection = db.GetRatingCollection();

            var aggregation = titleCollection.Aggregate().Match(Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Regex("primaryTitle", new BsonRegularExpression("Blade Runner")),
                Builders<BsonDocument>.Filter.Eq("startYear", 1982)))
                .Lookup("Rating", "_id", "_id", "ratings")
                .Unwind("ratings")
                .Group(new BsonDocument
                {
                    { "_id", "$_id"},
                    { "title", new BsonDocument("$first", "$primaryTitle")},
                    {
                        "rating", new BsonDocument("$push", new BsonDocument
                        {
                            { "averageRating", "$ratings.averageRating" },
                            { "numVotes", "$ratings.numVotes" }
                        })
                    }
                });

            var result = aggregation.ToList();

            if (result.Count > 0)
            {
                // Aktualizuj dokument z tablicą ocen
                var filter = Builders<BsonDocument>.Filter.Eq("_id", result[0]["_id"]);
                var update = Builders<BsonDocument>.Update.Set("rating", result[0]["rating"]);
                var updateResult = titleCollection.UpdateOne(filter, update);

                // Sprawdzenie rezultatu aktualizacji
                if (updateResult.ModifiedCount > 0)
                {
                    Console.WriteLine("Dokument został zaktualizowany");
                }
                else
                {
                    Console.WriteLine("Dokument nie został zaktualizowany");
                }
            }
            else
            {
                Console.WriteLine("Nie ma takiego dokumentu w kolekcji Title");
            }

            Console.WriteLine("\n");
        }
    }
}
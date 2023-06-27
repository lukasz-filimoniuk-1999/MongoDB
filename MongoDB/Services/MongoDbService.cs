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

            var filter = Builders<BsonDocument>.Filter.And(
                          Builders<BsonDocument>.Filter.Eq("startYear", 2010),
                          Builders<BsonDocument>.Filter.Regex("genres", new BsonRegularExpression("Romance")),
                          Builders<BsonDocument>.Filter.Gt("runtimeMinutes", 90),
                          Builders<BsonDocument>.Filter.Lte("runtimeMinutes", 120));

            var projection = Builders<BsonDocument>.Projection.
                Include("primaryTitle")
                .Include("startYear")
                .Include("genres")
                .Include("runtimeMinutes")
                .Exclude("_id");

            var sort = Builders<BsonDocument>.Sort.Ascending("primaryTitle");

            var limitedResult = titleCollection.Find(filter).Project(projection).Sort(sort).Limit(5).ToList();

            limitedResult.ForEach(x => Console.WriteLine(x));

            Console.WriteLine("\n");
            var countDocuments = titleCollection.CountDocuments(filter);
            Console.WriteLine("Liczba dokumentów spełniających warunki: " + countDocuments);
            Console.WriteLine("\n");
        }

        public void Exercise3()
        {
            Console.WriteLine("Zadanie 3#");

            var titleCollection = db.GetTitleCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("startYear", 2000);
            var aggregation = titleCollection.Aggregate()
                                .Match(filter)
                                .Group(new BsonDocument
                                {
                                    { "_id", "$titleType" },
                                    { "count", new BsonDocument("$sum", 1) }
                                });

            var results = aggregation.ToList();
            foreach (var result in results)
            {
                string titleType = result["_id"].AsString;
                int count = result["count"].AsInt32;

                Console.WriteLine($"Typ: {titleType}, Liczba filmów: {count}");
            }

            Console.WriteLine("\n");
        }

        public void Exercise4()
        {
            Console.WriteLine("Zadanie 4#");
            var titleCollection = db.GetTitleCollection();
            var ratingCollection = db.GetRatingCollection();

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.In("startYear", new[] { 1999, 2000 }),
                Builders<BsonDocument>.Filter.Regex("genres", new BsonRegularExpression("Documentary")));

            var projection = Builders<BsonDocument>.Projection
                .Include("primaryTitle")
                .Include("startYear")
                .Include("avg_rating.averageRating")
                .Exclude("_id");

            var sort = Builders<BsonDocument>.Sort.Descending("avg_rating.averageRating");

            var documentCount = titleCollection.CountDocuments(filter);

            Console.WriteLine("Liczba dokumentów spełniających warunki: " + documentCount + "\n");

            var limitedCollection = titleCollection.Aggregate().Match(filter)
                .Lookup("Rating", "tconst", "tconst", "avg_rating")
                .Project(projection)
                .Sort(sort)
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

            var filter = Builders<BsonDocument>.Filter.Text("Fonda Coppola", new TextSearchOptions { CaseSensitive = true });

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
            Console.WriteLine("Zadanie 7#");
            var titleCollection = db.GetTitleCollection();
            var ratingCollection = db.GetRatingCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("averageRating", 10.0);
            var results = ratingCollection.Find(filter).ToList();

            int updateDocumentsCount = 0;
            foreach (var result in results)
            {
                // Aktualizuj dokument z tablicą ocen
                int i = 0;
                var updateFilter = Builders<BsonDocument>.Filter.Eq("tconst", result["tconst"]);
                var update = Builders<BsonDocument>.Update.Set("max", 1);
                var updateResult = titleCollection.UpdateOne(updateFilter, update);
                updateDocumentsCount++;
            }

            Console.WriteLine("Liczba zaktualizowanych dokumentów: " + updateDocumentsCount);
            Console.WriteLine("\n");
        }

        public void Exercise8()
        {
            Console.WriteLine("Zadanie 8#");
            var titleCollection = db.GetTitleCollection();

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("primaryTitle", "The Derby 1895"),
                Builders<BsonDocument>.Filter.Eq("startYear", 1895));

            var projection = Builders<BsonDocument>.Projection
                .Include("primaryTitle")
                .Include("startYear")
                .Include("avg_rating.averageRating")
                .Exclude("_id");

            var titleInfoDocument = titleCollection.Aggregate().Match(filter)
                .Lookup("Rating", "tconst", "tconst", "avg_rating")
                .Project(projection)
                .ToList();

            titleInfoDocument.ForEach(d => Console.WriteLine(d));
            Console.WriteLine("\n");
        }

        public void Exercise9()
        {
            Console.WriteLine("Zadanie 9#");
            var titleCollection = db.GetTitleCollection();
            var ratingCollection = db.GetRatingCollection();

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("primaryTitle", "Blade Runner"),
                Builders<BsonDocument>.Filter.Eq("startYear", 1982));

            var aggregation = titleCollection.Aggregate().Match(filter)
                .Lookup("Rating", "tconst", "tconst", "ratings")
                .Unwind("ratings")
                .Group(new BsonDocument
                {
                    { "_id", "$tconst"},
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
                var updateFilter = Builders<BsonDocument>.Filter.Eq("tconst", result[0]["_id"]);
                var update = Builders<BsonDocument>.Update.Set("rating", result[0]["rating"]);
                var updateResult = titleCollection.UpdateOne(updateFilter, update);

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

        public void Exercise10()
        {
            Console.WriteLine("Zadanie 10#");
            var titleCollection = db.GetTitleCollection();

            var resultFilter = Builders<BsonDocument>.Filter.And(
                          Builders<BsonDocument>.Filter.Eq("primaryTitle", "Blade Runner"),
                          Builders<BsonDocument>.Filter.Eq("startYear", 1982));

            var result = titleCollection.Find(resultFilter).ToList();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", result[0]["_id"]);
            var update = Builders<BsonDocument>.Update.Set("rating", result[0]["rating"].AsBsonArray.Add(new BsonDocument {
                                                                                                              { "averageRating", 10 },
                                                                                                              { "numVotes", 12345 }
                                                                                                                }));
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

        public void Exercise11()
        {
            Console.WriteLine("Zadanie 11#");
            var titleCollection = db.GetTitleCollection();

            var resultFilter = Builders<BsonDocument>.Filter.And(
                          Builders<BsonDocument>.Filter.Eq("primaryTitle", "Blade Runner"),
                          Builders<BsonDocument>.Filter.Eq("startYear", 1982));

            var result = titleCollection.Find(resultFilter).ToList();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", result[0]["_id"]);
            var update = Builders<BsonDocument>.Update.Unset("rating");
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

        public void Exercise12()
        {
            Console.WriteLine("Zadanie 12#");

            var titleCollection = db.GetTitleCollection();

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("primaryTitle", "Pan Tadeusz"),
                Builders<BsonDocument>.Filter.Eq("startYear", 1999));

            var update = Builders<BsonDocument>.Update.Set("avgRating", 9.1);

            var options = new UpdateOptions { IsUpsert = true };

            titleCollection.UpdateOne(filter, update, options);

            Console.WriteLine("\n");
        }

        public void Exercise13()
        {
            Console.WriteLine("Zadanie 13#");

            var titleCollection = db.GetTitleCollection();
            var filter = Builders<BsonDocument>.Filter.Lt("startYear", 1989);

            var result = titleCollection.DeleteMany(filter);

            Console.WriteLine($"Liczba usuniętych dokumentów: {result.DeletedCount}");
            Console.WriteLine("\n");
        }
    }
}
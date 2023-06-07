using DBClient.Data;
using DBClient.Models;

using MongoDB.Driver;
using MongoDB.Bson;

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

            Console.WriteLine("#Zadanie 1.");
            string result = $"Rating: {ratingCount}, Name: {nameCount}, Title: {titleCount}";
            Console.WriteLine(result + "\n");
        }

        public void Exercise2(int StartYearParam, string CategoryParam, int runtimeMinutesParam1, int runtimeMinutesParam2)
        {
            Console.WriteLine("#Zadanie 2.");
            var titleCollection = db.GetTitleCollection();

            // Ustawienie filtru do tego zadania

            var Filters = Builders<Title>.Filter.And(
                Builders<Title>.Filter.Eq("startYear", StartYearParam),
                Builders<Title>.Filter.Regex("genres", new BsonRegularExpression(CategoryParam, "i")),
                Builders<Title>.Filter.Gt("runtimeMinutes", runtimeMinutesParam1),
                Builders<Title>.Filter.Lte("runtimeMinutes", runtimeMinutesParam2)
                );


            List<Title> TitleDocumentsList = titleCollection.Find(Filters).Limit(5).ToList();
            var titleDocumentCountByFilters = titleCollection.Find(Filters).CountDocuments();

            foreach(var document in TitleDocumentsList)
            {
                Console.WriteLine(document.ToJson());
            }

            Console.WriteLine("Liczba dokumentów spełniających ten warunek: " + titleDocumentCountByFilters + ". \n");
        }

        public void Exercise3()
        {
            throw new NotImplementedException();
        }

        public void Exercise4()
        {
            throw new NotImplementedException();
        }

        public void Exercise5()
        {
            throw new NotImplementedException();
        }

        public void Exercise6()
        {
            throw new NotImplementedException();
        }

        public void Exercise7()
        {
            throw new NotImplementedException();
        }

        public void Exercise8()
        {
            throw new NotImplementedException();
        }

        public void Exercise9()
        {
            throw new NotImplementedException();
        }

        public void Exercise10()
        {
            throw new NotImplementedException();
        }

        public void Exercise11()
        {
            throw new NotImplementedException();
        }

        public void Exercise12()
        {
            throw new NotImplementedException();
        }

        public void Exercise13()
        {
            throw new NotImplementedException();
        }

    }
}

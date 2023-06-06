using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

using DBClient.Models;

namespace DBClient
{
    class Program
    {
        static IConfigurationRoot configuration;
        static IMongoDatabase db;

        static void Main(string[] args)
        {
            SetConfig();
            SetDbConnection();

            var collection = db.GetCollection<Rating>("Rating");
            var filter = Builders<Rating>.Filter.Empty;
            var documents = collection.Find(filter).ToList();
            Console.WriteLine(documents);
        }
      
        static void SetConfig()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("config\\settings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        static void SetDbConnection()
        {
            var client = new MongoClient(configuration["db:connectionString"]);
            db = client.GetDatabase(configuration["db:dbName"]);
        }
    }
}

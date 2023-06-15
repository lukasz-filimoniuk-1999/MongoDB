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

using DBClient.Services;
using DBClient.Data;
using DBClient.Import;

namespace DBClient
{
    class Program
    {
        static IConfigurationRoot configuration;
        static IMongoDatabase db;
        static IImportService importService;
        static IMongoDbService mongoService;

        static void Main(string[] args)
        {
            SetConfig();
            SetDbConnection();

            ConfigureServices();

            // Import danych, zakomentowany jesli już są dane
            // importService.ImportData();

            // Wywołanie zadania 1
            mongoService.Exercise2();

            while (true);
        }

        /// <summary>
        /// Wczytanie pliku konfiguracyjjnego.
        /// </summary>
        static void SetConfig()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("config\\settings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        /// <summary>
        /// Ustawienie połączenia z bazą danych MongoDb.
        /// </summary>
        static void SetDbConnection()
        {
            var client = new MongoClient(configuration["db:connectionString"]);
            db = client.GetDatabase(configuration["db:dbName"]);
        }

        /// <summary>
        /// Wczytanie serwisów.
        /// </summary>
        static void ConfigureServices()
        {
            importService = new ImportService(configuration);

            IMongoDbDataProvider mongoDbDataProvider = new MongoDbDataProvider(db);
            mongoService = new MongoDbService(mongoDbDataProvider);
        }   
    }
}

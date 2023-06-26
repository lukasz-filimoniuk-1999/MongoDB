using System;

using MongoDB.Driver;

using Microsoft.Extensions.Configuration;

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

            // Wywołanie zadania 1.
            //mongoService.Exercise1();

            // Wywołanie zadania 2.
            //mongoService.Exercise2(2010, "Romance", 90, 120);

            // Wywołanie zadania 3.
            //mongoService.Exercise3(2000);

            // Wywołanie zadania 4.
            //mongoService.Exercise4(1999, 2000, "Documentary");

            // Wywołanie zadania 5.
            //mongoService.Exercise5("Fonda Coppola");

            // Wywołanie zadania 6.
            //mongoService.Exercise6();

            // Wywołanie zadania 7.
            mongoService.Exercise7();

            // Wywołanie zadania 8.
            //mongoService.Exercise8("The Derby 1895", 1895);

            // Wywołanie zadania 9.
            //mongoService.Exercise9("Blade Runner", 1982);

            // Wywołanie zadania 10.
            //mongoService.Exercise10("Blade Runner", 1982);

            // Wywołanie zadania 11.
            //mongoService.Exercise11("Blade Runner", 1982);

            //Wywołanie zadania 12.
            //mongoService.Exercise12("Pan Tadeusz", 1999);

            //Wywołanie zadania 13.
            //mongoService.Exercise13(1989);

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

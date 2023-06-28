﻿using System;

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

            // Importowanie danych (zakomentowany, jeśli już są dane)
            importService.ImportData();

            // Wywołanie zadania 1.
            mongoService.Exercise1();

            // Wywołanie zadania 2.
            mongoService.Exercise2();

            // Wywołanie zadania 3.
            mongoService.Exercise3();

            // Wywołanie zadania 4.
            mongoService.Exercise4();

            // Wywołanie zadania 5.
            mongoService.Exercise5();

            // Wywołanie zadania 6.
            mongoService.Exercise6();

            // Wywołanie zadania 7.
            mongoService.Exercise7();

            // Wywołanie zadania 8.
            mongoService.Exercise8();

            // Wywołanie zadania 9.
            mongoService.Exercise9();

            // Wywołanie zadania 10.
            mongoService.Exercise10();

            // Wywołanie zadania 11.
            mongoService.Exercise11();

            //Wywołanie zadania 12.
            mongoService.Exercise12();

            //Wywołanie zadania 13.
            mongoService.Exercise13();

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

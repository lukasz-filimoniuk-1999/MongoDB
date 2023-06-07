using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient.Import
{
    public class ImportService : IImportService
    {
        private readonly IConfigurationRoot configuration;

        public ImportService(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public void ImportData()
        {
            Console.WriteLine("1. Komunikat dotyczący importu danych do katalogu Title");
            ImportCollection("Title", configuration["import:tsvTitleFilePath"]);

            Console.WriteLine("2. Komunikat dotyczący importu danych do katalogu Name");
            ImportCollection("Name", configuration["import:tsvNameFilePath"]);

            Console.WriteLine("3. Komunikat dotyczący importu danych do katalogu Rating");
            ImportCollection("Rating", configuration["import:tsvRatingFilePath"]);
        }

        private void ImportCollection(string collectionName, string tsvFilePath)
        {
            string databaseName = configuration["db:dbName"];

            //Tworzenie procesu mongoimport
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = configuration["import:mongoImportPath"],
                Arguments = $"--db {databaseName} --collection {collectionName} --type tsv --file \"{tsvFilePath}\" --headerline"
            };

            //Uruchomienie procesu mongoimport
            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();
            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                Console.WriteLine("Importowanie danych zakończone powodzeniem.");
            }
            else
            {
                Console.WriteLine("Wystąpił błąd podczas importowania danych.");
            }
        }
    }
}

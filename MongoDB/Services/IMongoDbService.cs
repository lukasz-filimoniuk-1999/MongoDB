using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient.Services
{
    /// <summary>
    /// Interfejs serwisu bazy danych IMDB MongoDb
    /// </summary>
    public interface IMongoDbService
    {
        /// <summary>
        /// Zadanie 1 
        /// <para>Sprawdź liczbę dokumentów w kolekcjach Title/Rating/Name.</para>
        /// </summary>
        void Exercise1();
        void Exercise2(int StartYearParam, string CategoryParam, int runtimeMinutesParam1, int runtimeMinutesParam2);
        void Exercise3(int YearParam);
        void Exercise4();
        void Exercise5();
        void Exercise6();
        void Exercise7();
        void Exercise8();
        void Exercise9();
        void Exercise10();
        void Exercise11();
        void Exercise12();
        void Exercise13();

        //TODO Dodać resztę metod
    }
}

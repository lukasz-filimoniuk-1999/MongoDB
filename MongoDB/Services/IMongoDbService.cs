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
        /// Zadanie 1.
        /// <para>Sprawdź liczbę dokumentów w kolekcjach Title/Rating/Name.</para>
        /// </summary>
        void Exercise1();

        /// <summary>
        /// Zadanie 2.
        /// <para> Wybierz 5 pierwszych dokumentów z kolekcji Title, które były wyprodukowane w 2010 roku, są z kategorii
        /// filmów Romance, ich czas trwania jest większy niż 90 minut, ale nie przekracza 120 minut. Zwracane dokumenty
        /// powinny zawierać tytuł, rok produkcji, kategorię oraz czas trwania. Dane uporządkuj rosnąco wg tytułu filmu.
        /// Sprawdź również, ile dokumentów zwróciłoby zapytanie po wyłączeniu ograniczenia w postaci 5 pierwszych dokumentów.
        /// Wyszukując łańcuchy, skorzystaj z operatora $regex.
        /// </para>
        /// </summary>

        void Exercise2(int startYear, string genres, int gtMinutes, int lteMinutes);

        void Exercise3(int startYear);

        void Exercise4(int startYearBegin, int startYearEnd, string genres);

        void Exercise5(string primaryName);

        void Exercise6();

        void Exercise7();

        void Exercise8(string primaryTitle, int startYear);

        void Exercise9(string primaryTitle, int startYear);

        void Exercise10(string primaryTitle, int startYear);

        void Exercise11(string primaryTitle, int startYear);

        void Exercise12(string primaryTitle, int startYear);

        void Exercise13(int ltStartYear);
    }
}

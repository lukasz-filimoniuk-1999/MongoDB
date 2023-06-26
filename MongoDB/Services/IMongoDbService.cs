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
        /// <para>
        /// Sprawdź liczbę dokumentów w kolekcjach Title/Rating/Name.
        /// </para>
        /// </summary>
        void Exercise1();

        /// <summary>
        /// Zadanie 2.
        /// <para> 
        /// Wybierz 5 pierwszych dokumentów z kolekcji Title, które były wyprodukowane w 2010 roku, są z kategorii
        /// filmów Romance, ich czas trwania jest większy niż 90 minut, ale nie przekracza 120 minut. Zwracane dokumenty
        /// powinny zawierać tytuł, rok produkcji, kategorię oraz czas trwania. Dane uporządkuj rosnąco wg tytułu filmu.
        /// Sprawdź również, ile dokumentów zwróciłoby zapytanie po wyłączeniu ograniczenia w postaci 5 pierwszych dokumentów.
        /// Wyszukując łańcuchy, skorzystaj z operatora $regex.
        /// </para>
        /// </summary>
        void Exercise2();

        /// <summary>
        /// Zadanie 3.
        /// <para>
        /// Sprawdź, ile filmów różnego typu (pole titleType) było wyprodukowanych w roku 2000. Wynik zapytania powinien
        /// zwracać nazwę typu oraz liczbę filmów.
        /// </para>
        /// </summary>
        void Exercise3();

        /// <summary>
        /// Zadanie 4.
        /// <para>
        /// W oparciu o kolekcje Title oraz Rating sprawdź średnią ocenę filmów dokumentalnych wyprodukowanych w latach 1999-2000.
        /// Wyświetl tytuł filmu, rok produkcji oraz jego średnią ocenę. Dane uporządkuj malejąco według średniej oceny.
        /// a) Sprawdź, ile takich dokumentów zwróci zapytanie.
        /// b) Wyświetl tylko 5 pierwszych dokumentów spełniających powyższe warunki.
        /// </para>
        /// </summary>
        void Exercise4();

        /// <summary>
        /// Zadanie 5.
        /// <para>
        /// Utwórz indeks tesktowy dla pola primaryName w kolekcji Name. Następnie używając tego indeksu, znajdź dokumenty opisujące
        /// osoby o nazwisku Fonda oraz Coppola. Przy wyszukiwaniu włącz opcję, która będzie uwzględniać wielkie/małe litery.
        /// a) Ile dokumentów zwraca zapytanie?
        /// b) Wyświetl 5 pierwszych dokumentów (dokument powinien zwracać dwa pola: primaryName, primaryProffesion).
        /// </para>
        /// </summary>
        void Exercise5();

        /// <summary>
        /// Zadanie 6.
        /// <para>
        /// Utwórz indeks z porządkiem malejącym dla pola birthYear (kolekcja Name). Następnie wyświetl listę indeksów w kolekcji Name.
        /// Ile indeksów posiada ta kolekcja?
        /// </para>
        /// </summary>
        void Exercise6();

        /// <summary>
        /// Zadanie 7.
        /// <para>
        /// Dla każdego filmu (kolekcja Title), który ma najwyższą średnią ocenę (10.0), dodaj pole max z wartością równą 1.
        /// W poleceniu skorzystaj z kolekcji Rating, który zawiera informacje o średniej ocenie filmu.
        /// </para>
        /// </summary>
        void Exercise7();

        /// <summary>
        /// Zadanie 8.
        /// <para>
        /// W oparciu o kolekcje Title oraz Rating sprawdź średnią ocenę dowolnego filmu o określonym tytule oraz roku produkcji.
        /// Zapytanie powinno zwrócić nazwę filmu, rok produkcji oraz średnią ocenę.
        /// </para>
        /// </summary>
        void Exercise8();

        /// <summary>
        /// Zadanie 9.
        /// <para>
        /// Do filmu Blade Runner z roku 1982 dodaj pole rating, które będzie tablicą dokumentów z polami: averageRating oraz numVotes.
        /// Wartości pól w zagnieżdżonym dokumencie powinny posiadać odpowiednie wartości pobrane z kolekcji Rating.
        /// </para>
        /// </summary>
        void Exercise9();

        /// <summary>
        /// Zadanie 10.
        /// <para>
        /// Zmodyfikuj pole rating w dokumencie z Zadania 9, dodając jeszcze jeden dokument z polami averageRating oraz numVotes
        /// oraz z wartościami: 10 oraz 12345.
        /// </para>
        /// </summary>
        void Exercise10();

        /// <summary>
        /// Zadanie 11.
        /// <para>
        /// Usuń rating dodane do filmu Blade Runner w Zadaniu 10.
        /// </para>
        /// </summary>
        void Exercise11();

        /// <summary>
        /// Zadanie 12.
        /// <para>
        /// Do filmu Pan Tadeusz z 1999 roku dodaj pole avgRating z wartościa 9.1. Jeśli nie ma takiego filmu, polecenie powinno zadziałać jak upsert.
        /// </para>
        /// </summary>
        void Exercise12();

        /// <summary>
        /// Zadanie 13.
        /// <para>
        /// Z kolekcji Title usuń dokumenty, które reprezentują filmy wyprodukowane przed 1989 rokiem. Ile takich dokumentów zostało usuniętych?
        /// </para>
        /// </summary>
        void Exercise13();
    }
}

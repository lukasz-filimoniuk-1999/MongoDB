using DBClient.Models;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient.Data
{
    /// <summary>
    /// Interfejs dostawcy danych z bazy IMDB MongoDb.
    /// </summary>
    public interface IMongoDbDataProvider
    {
        /// <summary>
        /// Zwraca kolekcję dokumentów z ocenami filmów.
        /// </summary>
        /// <returns>Kolekcja dokumentów</returns>
        IMongoCollection<Rating> GetRatingCollection();

        /// <summary>
        /// Zwraca kolekcję dokumentów z tytułami.
        /// </summary>
        /// <returns>Kolekcja dokumentów</returns>
        IMongoCollection<Title> GetTitleCollection();

        /// <summary>
        /// Zwraca kolekcję dokumentów z nazwami.
        /// </summary>
        /// <returns>Kolekcja dokumentów</returns>
        IMongoCollection<Name> GetNameCollection();

    }
}

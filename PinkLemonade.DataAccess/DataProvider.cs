using PinkLemonade.DataAccess.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace PinkLemonade.DataAccess
{
    public static class DataProvider
    {
        private static string _dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "database.db3");

        private static SQLiteConnection _connection;

        private static SQLiteConnection _db
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection(_dbPath);
                    CreateTables();
                }

                return _connection;
            }
        }

        public static List<StoredToken> GetTokens()
        {
            List<StoredToken> results = new List<StoredToken>();


            foreach (var item in _db.Table<StoredToken>())
                results.Add(item);


            return results;
        }

        public static StoredToken GetToken(int id)
        {

            return _db.Get<StoredToken>(id);

        }

        public static int StoreToken(StoredToken token)
        {
            return _db.Insert(token);
        }

        public static void DeleteToken(int id)
        {
            _db.Delete<StoredToken>(id);
        }

        private static void CreateTables()
        {
            _db.CreateTable<StoredToken>();

            
        }
    }
}

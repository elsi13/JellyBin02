using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace JellyBin02
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Bin>().Wait();
        }

        public Task<List<Bin>> GetPeopleAsync()
        {
            return _database.Table<Bin>().ToListAsync();
        }

        public Task<int> SavePersonAsync(Bin person)
        {
            return _database.InsertAsync(person);
        }
    }
}
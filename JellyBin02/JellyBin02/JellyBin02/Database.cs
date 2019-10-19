using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace JellyBin02

{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        private List<Bin> _seedBinList = new List<Bin>
        {
            new Bin { longit = -4.294466, lat = 55.873912, isFull = false, colorID = 1},
            new Bin { longit = -4.295075 , lat = 55.874898, isFull = false, colorID = 2},
            new Bin { longit = -4.293517 , lat = 55.873496, isFull = false, colorID = 3},
            new Bin { longit = -4.294257, lat = 55.872759, isFull = false, colorID = 4},
            new Bin { longit =  -4.291435 , lat = 55.872711, isFull = true, colorID = 5},
            new Bin { longit =  -4.288570 , lat = 55.873078 , isFull = false, colorID = 6},
            new Bin { longit =  -4.286134, lat = 55.874011, isFull = true, colorID = 0},
            new Bin { longit = -4.283688, lat = 55.873686, isFull = false, colorID = 1},
            new Bin { longit = -4.286724, lat = 55.873536, isFull = false, colorID = 4},
            new Bin { longit = -4.287175, lat = 55.877081, isFull = false, colorID = 3}

        };


        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Bin>().Wait();
        }

        public async Task<List<Bin>> GetBinAsync()
        {
             await _database.InsertAllAsync(_seedBinList);
            System.Console.WriteLine(_seedBinList);
            return await _database.Table<Bin>().ToListAsync();

        }

        public Task<int> SaveBinAsync(Bin bin)
        {
            return _database.InsertAsync(bin);
        }
    }
}
using SQLite;
using Xamarin.Forms;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace JellyBin02

{
    public class Database
    {
        private SQLiteAsyncConnection _database;
        public SQLiteOpenFlags CreationFlags { get; } =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache |
        SQLiteOpenFlags.FullMutex;

        private List<Bin> _seedBinList = new List<Bin>
        {
            new Bin { longit = -4.294466, lat = 55.873912, isFull = "empty", colorID = 1},
            new Bin { longit = -4.295075 , lat = 55.874898, isFull = "empty", colorID = 2},
            new Bin { longit = -4.293517 , lat = 55.873496, isFull = "empty", colorID = 3},
            new Bin { longit = -4.294257, lat = 55.872759, isFull = "empty", colorID = 4},
            new Bin { longit =  -4.291435 , lat = 55.872711, isFull = "full", colorID = 0},
            new Bin { longit =  -4.288570 , lat = 55.873078 , isFull = "empty", colorID = 4},
            new Bin { longit =  -4.286134, lat = 55.874011, isFull = "full", colorID = 0},
            new Bin { longit = -4.283688, lat = 55.873686, isFull = "empty", colorID = 1},
            new Bin { longit = -4.286724, lat = 55.873536, isFull = "empty", colorID = 4},
            new Bin { longit = -4.287175, lat = 55.877081, isFull = "empty", colorID = 3}

        };


        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath(dbPath), CreationFlags);
            Console.WriteLine(_database.GetConnection());
            _database.CreateTableAsync<Bin>().Wait();
        }

        public async Task<List<Bin>> GetBinAsync()
        {
            if ((await _database.Table<Bin>().CountAsync() == 0))
            {
                _database.InsertAllAsync(_seedBinList).Wait();
                _database.UpdateAllAsync(_seedBinList).Wait();
            }

            return await _database.GetAllWithChildrenAsync<Bin>();

        }

        //SAVE Bin
        public async Task<int> SaveBinAsync(Bin bin)
        {
            await _database.InsertWithChildrenAsync(bin);
            return bin.ID;
        }

        //DELETE ITEM
        public Task DeleteItem<T>(T itemToDelete)
        {
            return _database.DeleteAsync(itemToDelete);
        }

        //UPDATE ITEM
        public async Task UpdateItem<T>(T item)
        {
            await _database.UpdateWithChildrenAsync(item);
        }
    }
}
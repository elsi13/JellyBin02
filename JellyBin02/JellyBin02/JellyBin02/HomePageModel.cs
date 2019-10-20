using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace JellyBin02
{
    public class HomePageModel
    {
        //Constructor
        public HomePageModel()
        {
            map = new Map();

            locations = new List<Bin>
            {
                new Bin { longit = -4.294466, lat = 55.873912, isFull = "empty", colorID = 1},
                new Bin { longit = -4.295075 , lat = 55.874898, isFull = "empty", colorID = 2},
                new Bin { longit = -4.293517 , lat = 55.873496, isFull = "empty", colorID = 3},
                new Bin { longit = -4.294257, lat = 55.872759, isFull = "empty", colorID = 4},
                new Bin { longit =  -4.291435 , lat = 55.872711, isFull = "full", colorID = 5},
                new Bin { longit =  -4.288570 , lat = 55.873078 , isFull = "empty", colorID = 6},
                new Bin { longit =  -4.286134, lat = 55.874011, isFull = "full", colorID = 0},
                new Bin { longit = -4.283688, lat = 55.873686, isFull = "empty", colorID = 1},
                new Bin { longit = -4.286724, lat = 55.873536, isFull = "empty", colorID = 4},
                new Bin { longit = -4.287175, lat = 55.877081, isFull = "empty", colorID = 3}

            };
        }

        //attributes
        public Map map;
        public List<Bin> locations;

        public async Task<List<Bin>> GetGroupedList()
        {
            return await App.database.GetBinAsync();
        }
    }
}

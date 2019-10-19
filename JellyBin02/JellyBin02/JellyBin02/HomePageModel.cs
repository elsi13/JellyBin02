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
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(55.8816253, -4.3175199),
                Distance.FromMiles(.5)));

            GetGroupedTodoList().ContinueWith(t =>
            {
                _locations = t.Result;
            });

            _locations = new List<Bin>
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

            for(int i = 0; i<_locations.Count-1; i++)
            {
                map.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = _locations[i].isFull.ToString(),
                    Position = new Position(_locations[i].lat, _locations[i].longit)

                });

            }
            

        }

        //attributes
        private Map map;
        private List<Bin> _locations;

        private async Task<List<Bin>> GetGroupedTodoList()
        {
            return await App.database.GetBinAsync();
        }
    }
}

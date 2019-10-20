using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using System.Reflection;
using Xamarin.Essentials;

//package in which we write the program     
namespace JellyBin02

{
    //name of the class (public=accessible by everything, partial = ? )
    public partial class HomePage : ContentPage
    {

        public HomePageModel Model { get; set; }
        public object BitmapDescriptorFactory { get; private set; }

        //constructor: initialise instance of the class
        public HomePage()
        {
            //built in 
            InitializeComponent();
            //bind together HomePage.xaml.cs with HomePageModel.cs
            Model = new HomePageModel();
            BindingContext = Model;



        }
        //void=dont return a value, just run. protected=subclasses can see it. overriding the parent method "OnAppearing" 
        //which is built into ContentPage. we override because we want to add functionality to the OnAppearing method.
        protected async override void OnAppearing()
        {
            //call parent function
            base.OnAppearing();

            Model.locations = await Model.GetGroupedList();


            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    /*map.Pins.Add(
                    new Xamarin.Forms.GoogleMaps.Pin
                    {
                        Type = Xamarin.Forms.GoogleMaps.PinType.Generic,
                        Label = "Me",
                        Position = new Xamarin.Forms.GoogleMaps.Position(location.Latitude, location.Longitude),
                        //Icon = BitmapDescriptorFactory.FromBundle("C:\Users\Eli\Desktop\JellyBin02\JellyBin02\JellyBin02\JellyBin02.Android\Resources\drawable\pin1.png")
                    });

                    await App.Current.MainPage.DisplayAlert("Your Location", $"Latitude: {location.Latitude}, Longitude: {location.Longitude}", "OK");*/
                    System.Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Model.MyLocation = location;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Failed", "Could not get your location.", "OK");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await App.Current.MainPage.DisplayAlert("Failed", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await App.Current.MainPage.DisplayAlert("Failed", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Failed", ex.Message, "OK");
            }




            map.MoveToRegion(Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(Model.MyLocation.Latitude, 
                Model.MyLocation.Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(.5)));
            map.MapClicked += OnMapClicked;

            AddTrash.Clicked += ButtonClicked;

            map.Pins.Add(
                new Xamarin.Forms.GoogleMaps.Pin
                {
                    Type = Xamarin.Forms.GoogleMaps.PinType.Place,
                    Label = "Me",
                    Position = new Xamarin.Forms.GoogleMaps.Position(Model.MyLocation.Latitude, Model.MyLocation.Longitude),
                    Icon = Xamarin.Forms.GoogleMaps.BitmapDescriptorFactory.DefaultMarker(Color.Aqua)
                    //Icon = BitmapDescriptorFactory.FromBundle("C:\Users\Eli\Desktop\JellyBin02\JellyBin02\JellyBin02\JellyBin02.Android\Resources\drawable\pin1.png")
                });


            for (int i = 0; i < Model.locations.Count; i++)
            {
            /*
            * 
            * 0 - black
            * 1 - green - food waste
            * 2 - purple - glass
            * 3 - brown = garden waste, food waste
            * 4 -  blue - paper, card, plastic bottles, cans
            */

            Color color;
                switch (Model.locations[i].colorID)
                {
                    case 0:
                        color = Color.Black;
                        break;
                    case 1:
                        color = Color.Green;
                        break;
                    case 2:
                        color = Color.Purple;
                        break;
                    case 3:
                        color = Color.Brown;
                        break;
                    case 4:
                        color = Color.Blue;
                        break;
                    default:
                        color = Color.Red;
                        break;
                }

                var pin = new Xamarin.Forms.GoogleMaps.Pin
                {
                    Type = Xamarin.Forms.GoogleMaps.PinType.Place,
                    Label = Model.locations[i].isFull.ToString(),
                    Position = new Xamarin.Forms.GoogleMaps.Position(Model.locations[i].lat, Model.locations[i].longit),
                    Icon = Xamarin.Forms.GoogleMaps.BitmapDescriptorFactory.DefaultMarker(color)

                };
                map.Pins.Add(pin);
                pin.Clicked += async (sender, e) =>
                {
                    string action = await DisplayActionSheet("Do you want to mark this bin as full?", "No", null, "Yes");
                    if (action == "Yes")
                    {
                        pin.Label = "full";
                        await App.database.UpdateItem<Bin>(Model.locations[i]);
                    }
                };

            }
        }

        public void ButtonClicked(object sender, EventArgs e)
        {
            if (Model.trashbool)
            {
                map.Opacity = 0.75;
                //map.MapType = Xamarin.Forms.GoogleMaps.MapType.Hybrid;
            }
            else
            {
                map.Opacity = 1;
                //map.MapType = Xamarin.Forms.GoogleMaps.MapType.Street;
            }
        }


        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            
            var pin = new Xamarin.Forms.GoogleMaps.Pin
            {
                Type = PinType.SavedPin,
                Label = "Trash",
                Position = e.Point,
                Icon = Xamarin.Forms.GoogleMaps.BitmapDescriptorFactory.DefaultMarker(Color.BlanchedAlmond)
            };
 
            if (Model.trashbool)
            {
                System.Diagnostics.Debug.WriteLine($"MapClick: {e.Point.Latitude}, {e.Point.Longitude}");


                map.Pins.Add(pin);
                //App.database.UpdateItem<Bin>(new Bin { colorID=0, isFull="empty", lat= e.Point.Latitude , longit= e.Point.Longitude}).Wait();

            }

        }


    }
}

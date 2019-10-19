using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using System.Reflection;

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
        protected override void OnAppearing()
        {
            //call parent function
            base.OnAppearing();

            map.MoveToRegion(Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(55.872110, -4.294449),
               Xamarin.Forms.GoogleMaps.Distance.FromMiles(.5)));

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

                map.Pins.Add(
            new Xamarin.Forms.GoogleMaps.Pin
            {
                Type = Xamarin.Forms.GoogleMaps.PinType.Place,
                Label = Model.locations[i].isFull.ToString(),
                Position = new Xamarin.Forms.GoogleMaps.Position(Model.locations[i].lat, Model.locations[i].longit),
                Icon = Xamarin.Forms.GoogleMaps.BitmapDescriptorFactory.DefaultMarker(color)
            });

            }
        }


    }
}

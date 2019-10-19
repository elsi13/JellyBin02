using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
//package in which we write the program     
namespace JellyBin02

{
    //name of the class (public=accessible by everything, partial = ? )
    public partial class HomePage : ContentPage
    {
        public HomePageModel Model { get; set; }

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
        protected override async void OnAppearing()
        {
            //call parent function
            base.OnAppearing();


            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(55.872110, -4.294449),
               Distance.FromMiles(.5)));
            
            for (int i = 0; i < Model._locations.Count - 1; i++)
            {
               
     
                var pin =new Pin
                {
                    Type = PinType.Place,
                    Label = Model._locations[i].isFull,
                    Position = new Position(Model._locations[i].lat, Model._locations[i].longit)

                };
                map.Pins.Add(pin);
                pin.Clicked += async (sender, e) =>
                {
                   string action =await DisplayActionSheet("Do you want to mark this bin as full?","Cancel", null, "Yes");
                    if (action == "Yes")
                    {
                        Model._locations[i].isFull = "full";
                        pin.Label="full";
                        
                    }
                };



            }
          
        }


    }
}

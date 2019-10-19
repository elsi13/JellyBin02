using System;
using System.IO;
using Xamarin.Forms;
//package in which we write the program     
namespace JellyBin02

{
    //name of the class (public=accessible by everything, partial = ? )
    public partial class HomePage : ContentPage
    {
        //constructor: initialise instance of the class
        public HomePage()
        {
            //built in 
            InitializeComponent();
            //bind together HomePage.xaml.cs with HomePageModel.cs
            BindingContext = new HomePageModel();


           
        }
        //void=dont return a value, just run. protected=subclasses can see it. overriding the parent method "OnAppearing" 
        //which is built into ContentPage. we override because we want to add functionality to the OnAppearing method.
        protected override async void OnAppearing()
        {
            //call parent function
            base.OnAppearing();
        }


    }
}

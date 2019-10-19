using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JellyBin02
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomePage();
        }
        // When we deploy the project to the phone, the first class which will be looked at is App, and InitializeComponent() initializes the 
        //main page of the app. This creates a connection to HomePage
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

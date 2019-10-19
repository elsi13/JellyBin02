using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JellyBin02
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        //create an attribute/field called Model which connects the xaml.cs file to the cs file
        public HomePageModel Model { get; set; }
        public HomePage()
        {
            InitializeComponent();
            //set the main functionality as the cs page.
            Model = new HomePageModel();
            BindingContext = Model;
        }
    }
}

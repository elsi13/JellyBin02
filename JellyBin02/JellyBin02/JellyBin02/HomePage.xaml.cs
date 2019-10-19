using System;
using System.IO;
using Xamarin.Forms;

namespace JellyBin02

{
    public partial class HomePage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");

        public HomePage()
        {
            InitializeComponent();

            if (File.Exists(_fileName))
            {
                editor.Text = File.ReadAllText(_fileName);
            }
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(_fileName, editor.Text);
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            editor.Text = string.Empty;
        }
    }
}

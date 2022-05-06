using System;
using AppFinal.Cash;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        

        public Profile()
        {
            InitializeComponent();
            
        }

        public void Logout_Clicked(object sender, EventArgs eventArgs)
        {
            CurrentUser.LogOff(); 
            Shell.Current.GoToAsync("LoginPage");
        }
    }
}
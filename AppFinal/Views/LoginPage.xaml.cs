using AppFinal.ViewModels;
using System;
using System.Collections.Generic;
using AppFinal.Interfaces;
using AppFinal.Models;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AppFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
        

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TxtUsername.Text))
            {
                await DisplayAlert("Ops!", "Username can't be empty", "OK");
            }
            else if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                await DisplayAlert("Ops!", "Password can't be empty", "OK");
            }
            else
            {

                var user =await User.Login(TxtUsername.Text, TxtPassword.Text);
                if (user != null)
                {
                    Console.WriteLine(user);
                    await Shell.Current.GoToAsync("Feed");
                }

            }

           

        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync( "Registration");
        }

        
    }
}
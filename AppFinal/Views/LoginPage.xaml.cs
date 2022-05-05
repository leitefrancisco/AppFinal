using AppFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppFinal.Cash;
using AppFinal.DB.AccessClasses;
using AppFinal.Interfaces;
using AppFinal.Models;
using Newtonsoft.Json;
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
            
            if (!string.IsNullOrEmpty(TxtUsername.Text))
            {
                await DisplayAlert("Ops!", "Username can't be empty", "OK");
            }
            else if (!string.IsNullOrEmpty(TxtPassword.Text))
            {
                await DisplayAlert("Ops!", "Password can't be empty", "OK");
            }
            else
            {
               
                CurrentUser.SetUser(await User.Login("2019405@student.cct.ie", "Pass123!")) ;

                Console.WriteLine(CurrentUser.GetUser());
                if (CurrentUser.GetUser() != null)
                {
                    await Shell.Current.GoToAsync("Profile");
                    

                    
                    

                }
                else
                {
                    await DisplayAlert("Ops!", "Wrong Email or Password", "OK");
                }
            
            }
           
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync( "Registration");
        }

    }
}
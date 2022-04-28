using AppFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFinal.DB.AccessClasses;
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
                //var authAPI = RestService.For<IAPI>("http://35.204.176.180:8080");
                //User user = new User
                //{
                //    Email = txtEmail.Text.ToString(),
                //    Password = txtPassword.Text.ToString()
                //};

                //Dictionary<string, string> data = new Dictionary<string, string>();
                //data.Add("email", user.Email);
                //data.Add("password", user.Password);
                //var result = await authAPI.SignIn(data);

                //var stringResult = result.ToString();
                //if (stringResult.Contains("Login Successful"))
                //{
                await Shell.Current.GoToAsync("Feed");
                //}
            }

           

        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync( "Registration");
        }
    }
}
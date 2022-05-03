using AppFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

            // if (string.IsNullOrEmpty(TxtUsername.Text))
            // {
            //     await DisplayAlert("Ops!", "Username can't be empty", "OK");
            // }
            // else if (string.IsNullOrEmpty(TxtPassword.Text))
            // {
            //     await DisplayAlert("Ops!", "Password can't be empty", "OK");
            // }
            // else
            // {
            //     var user = await User.Login("2019405@student.cct.ie", "Pass123!");
            //     Console.WriteLine("AQUI O logged in: " + user);
            //     
            //     Console.WriteLine(user);
            //     if (user != null)
            //     {
            //         Console.WriteLine(user);
            //         await Shell.Current.GoToAsync("Feed");
            //     }
            //
            // }
            var _httpClient = new HttpClient();
            var users = await GetJsonHttpClient("http://35.204.176.180:8080/users",_httpClient);
            

        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync( "Registration");
        }


        private static async Task<User> GetJsonHttpClient(string uri, HttpClient httpClient)
        {
            
            try
            {
                return await httpClient.GetFromJsonAsync<User>(uri);
            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (JsonException) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
            }

            return null;
        }


    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using AppFinal.Cash;
using AppFinal.Models;
using AppFinal.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Friends : ContentPage
    {
        public Friends()
        {
            InitializeComponent();
            FillGridAllFriends();
        }

        public async Task FillGridAllFriends()
        {
            var users = await CurrentUser.GetFriends();
            foreach (var user in users)
            {
                FillGrid(user);
            }
        }

        public void FillGrid(User user)
        {
            //creates a grid and its definitions
            var newGrid = new Grid
            {
                Margin = new Thickness(10, 10, 10, 10)
            };

            var rDef1 = new RowDefinition();
            var rDef2 = new RowDefinition();
            var rDef3 = new RowDefinition();
            
            rDef1.Height = 100;
            rDef2.Height = 100;
            rDef3.Height = 10;
            
            newGrid.RowDefinitions.Add(rDef1);
            newGrid.RowDefinitions.Add(rDef2);
            newGrid.RowDefinitions.Add(rDef3);
            
            //add the image
            var image = new Image
            {
                Source = (user.pictureUrl),
                Aspect = Aspect.AspectFit,
                HeightRequest = 200,
                WidthRequest = 200
            };
            
            Grid.SetRowSpan(image,2);
            Grid.SetRow(image,0);
            Grid.SetColumn(image,0);

            newGrid.Children.Add(image);

            //label with username
            var labelUsername = new Label
            {
                Text = user.username,
                Margin = new Thickness(10, 50, 20, 20)
            };
            Grid.SetColumn(labelUsername, 1);
            Grid.SetRow(labelUsername, 0);

            newGrid.Children.Add(labelUsername);

            //button to see profile
            var btnSeeProfile = new Xamarin.Forms.Button
            {
                Margin = new Thickness(10, 0, 10, 20),
                Text = "See Profile",
                BackgroundColor = Color.DarkGreen,
                BindingContext = user.id.ToString()
            };
            btnSeeProfile.Clicked += async (sender, args) =>
            {
                string data = ((Button)sender).BindingContext as string;
                var friend =await CurrentUser.GetFriend(data);
                CurrentFriend.SetUser(friend);
                await AppShell.Current.GoToAsync("FriendProfileView");
            };

            Grid.SetColumn(btnSeeProfile,1);
            Grid.SetRow(btnSeeProfile,1);

            newGrid.Children.Add(btnSeeProfile);

            //button to see profile
            var btnSendMessage = new Button
            {
                Margin = new Thickness(10, 0, 10, 20),
                Text = "Messages",
                BackgroundColor = Color.DarkGreen,
                BindingContext = user.id.ToString()
            };

            btnSendMessage.Clicked += async (sender, args) =>
            {
                string data = ((Button)sender).BindingContext as string;
                var friend = await CurrentUser.GetFriend(data);
                CurrentFriend.SetUser(friend);
                await AppShell.Current.GoToAsync("MessagesView");
            };


            Grid.SetColumn(btnSendMessage, 2);
            Grid.SetRow(btnSendMessage, 1);

            newGrid.Children.Add(btnSendMessage);

            //separator for each friend
            var separator = new Label();
            separator.Text = "___________________________________________________________";
            btnSeeProfile.BackgroundColor = Color.DarkGreen;
            separator.HeightRequest = 100;
            Grid.SetRow(separator, 3);
            Grid.SetColumnSpan(separator,3);

            newGrid.Children.Add(separator);
              

            MainLayout.Children.Add(newGrid);

        }
        
    }
}
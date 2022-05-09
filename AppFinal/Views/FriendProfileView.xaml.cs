using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFinal.Cash;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendProfileView : ContentPage
    {
        public FriendProfileView()
        {
            InitializeComponent();
            AddInteractionButton();
        }


        public void AddInteractionButton()
        {
            if (IsFriend()) AddMessageButton();
            else
            {
                AddFriendRequestButton();
            }
        }

        private async Task AddFriendRequestButton()
        {   

            InteractiveBtn.Text =  CurrentFriend.GetUser().friendsRequest.Contains(CurrentUser.GetUser().id) ? "Request Sent" : "Add Friend" ;

            if (InteractiveBtn.Text.Equals("Request Sent"))
            {
                InteractiveBtn.IsEnabled = false;
                InteractiveBtn.BackgroundColor = Color.DimGray;
                return;
            }
            
            InteractiveBtn.Clicked += async (sender, args) =>
            {
                await CurrentUser.GetUser().RequestFriendship(CurrentFriend.GetUser().id);
                InteractiveBtn.Text = "Request Sent";
                await DisplayAlert("Great!", "Request Sent!", "OK");
                CurrentFriend.GetUser().friendsRequest.AddLast(CurrentUser.GetUser().id);
                InteractiveBtn.IsEnabled = false;
                InteractiveBtn.BackgroundColor = Color.DimGray;


            };

        }

        private void AddMessageButton()
        {
            InteractiveBtn.Text = "Send Message";


            InteractiveBtn.Clicked += async (sender, args) =>
            {
                await AppShell.Current.GoToAsync("MessagesView");
            };
        }

        private bool IsFriend()
        {
            return CurrentUser.GetUser().friends.Contains(CurrentFriend.GetUser().id);
        }

        
    }
}
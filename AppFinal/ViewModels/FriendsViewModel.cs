    using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
    using AppFinal.Cash;
    using AppFinal.Models;
using AppFinal.Views;
using Xamarin.Forms;

namespace AppFinal.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
    //     private Friend _selectedFriend;
    //
    //     public ObservableCollection<Friend> Friends { get; }
    //     public Command LoadFriendsCommand { get; }
    //     public Command AddFriendCommand { get; }
    //     public Command<Friend> FriendTapped { get; }
    //

    public string UserPicture { get; } = CurrentUser.GetUser().pictureUrl;
    public string UserPicture2 { get; } = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fdalealbo.cl%2F__export%2F1615331541531%2Fsites%2Fdalealbo%2Fimg%2F2021%2F03%2F09%2Ffelipe-flores_1054495_crop1615331539727.jpg_497593902.jpg&f=1&nofb=1";
        public FriendsViewModel()
    {
        Title = "Friends";



            // Friends = new ObservableCollection<Friend>();
            // LoadFriendsCommand = new Command(async () => await ExecuteLoadFriendsCommand());
            // FriendTapped = new Command<Friend>(OnFriendSelected);
            // AddFriendCommand = new Command(OnAddFriend);
    }

        // <Grid ColumnDefinitions = "*,*" Margin="10,10,10,10" >
        // <Grid.RowDefinitions>
        // <RowDefinition Height = "100" />
        //     < RowDefinition Height="100"/>
        //     <RowDefinition Height = "1" />
        //     </ Grid.RowDefinitions >
        //
        // < Image Source="{Binding UserPicture}" Aspect="AspectFit" Grid.Column="0" Grid.Row="0" Grid.RowSpan="2" HeightRequest="200" WidthRequest="200" />
        // <Label Text = "Seu Juca" Grid.Column="1" Grid.Row="0" Margin="10,50,20,20"></Label>
        // <Button Grid.Row="1" Grid.Column= "1" Margin= "10,0,10,20" Text= "Send Message" BackgroundColor= "#003638" ></ Button >
        // < Label BackgroundColor= "#003638" Grid.Row= "2" Grid.ColumnSpan= "2" ></ Label >
        // </ Grid >


        public void FillFriendView()
        {
            
        }



        //methodo q cria as labels



        //
        //     async Task ExecuteLoadFriendsCommand()
        //     {
        //         IsBusy = true;
        //
        //         try
        //         {
        //             Friends.Clear();
        //             var friends = await DataStore.GetFriendAsync(true);
        //             foreach (var friend in friends)
        //             {
        //                 Friends.Add(friend);
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             Debug.WriteLine(ex);
        //         }
        //         finally
        //         {
        //             IsBusy = false;
        //         }
        //     }
        //
        //     public void OnAppearing()
        //     {
        //         IsBusy = true;
        //         SelectedFriend = null;
        //     }
        //
        //     public Friend SelectedFriend
        //     {
        //         get => _selectedFriend;
        //         set
        //         {
        //             SetProperty(ref _selectedFriend, value);
        //             OnFriendSelected(value);
        //         }
        //     }
        //
        //     private async void OnAddFriend(object obj)
        //     {
        //         await Shell.Current.GoToAsync(nameof(NewFriend));
        //
        //     }
        //
        //     async void OnFriendSelected(Friend friend)
        //     {
        //         if (friend == null)
        //             return;
        //         await Shell.Current.GoToAsync($"{nameof(FriendDetail)}?{nameof(FriendDetailViewModel.FriendId)}={friend.Id}");
        //     }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFinal.Models;
using AppFinal.Views;
using Xamarin.Forms;

namespace AppFinal.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        private Friend _selectedFriend;

        public ObservableCollection<Friend> Friends { get; }
        public Command LoadFriendsCommand { get; }
        public Command AddFriendCommand { get; }
        public Command<Friend> FriendTapped { get; }

        public FriendsViewModel()
        {
            Title = "Friends List";
            Friends = new ObservableCollection<Friend>();
            LoadFriendsCommand = new Command(async () => await ExecuteLoadFriendsCommand());
            FriendTapped = new Command<Friend>(OnFriendSelected);
            AddFriendCommand = new Command(OnAddFriend);
        }

        async Task ExecuteLoadFriendsCommand()
        {
            IsBusy = true;

            try
            {
                Friends.Clear();
                var friends = await DataStore.GetFriendAsync(true);
                foreach (var friend in friends)
                {
                    Friends.Add(friend);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedFriend = null;
        }

        public Friend SelectedFriend
        {
            get => _selectedFriend;
            set
            {
                SetProperty(ref _selectedFriend, value);
                OnFriendSelected(value);
            }
        }

        private async void OnAddFriend(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewFriend));

        }

        async void OnFriendSelected(Friend friend)
        {
            if (friend == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(FriendDetail)}?{nameof(FriendDetailViewModel.FriendId)}={friend.Id}");
        }
    }
}

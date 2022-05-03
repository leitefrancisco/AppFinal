using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFinal.Models;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppFinal.ViewModels
{
    [QueryProperty(nameof(FriendId), nameof(FriendId))]
    public class FriendDetailViewModel : BaseViewModel
    {
        private string friendId;
        private string name;
        private string description;

        public string Id { get; set; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string FriendId
        {
            get
            {
                return friendId;
            }
            set
            {
                friendId = value;
                LoadFriendId(value);
            }
        }

        public async void LoadFriendId(string friendId)
        {
            try
            {
                var friend = await DataStore.GetFriendAsync(friendId);
                /**               
                Id = friend.Id;
                Name = friend.Name;
                Description = friend.Description;
                **/

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Friend");
            }
        }
    }
}

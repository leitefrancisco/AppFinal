using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppFinal.Cash;

namespace AppFinal.ViewModels
{
    internal class FriendProfileViewModel : BaseViewModel
    {
        public string UserName { get; } = CurrentFriend.GetUser().username;
        public string UserPicture { get; } = CurrentFriend.GetUser().pictureUrl;
        public int FriendsAmt { get; } = CurrentFriend.GetUser().friends.Count;
        public int Matches { get; set; }
        public string UserRegion { get; } = CurrentFriend.GetUser().region;
        public string teste { get; } = "teste";

        public FriendProfileViewModel()
        {
            Title = " Profile";
            SetMatches();

        }

        private async Task SetMatches()
        {
            var matches = await CurrentFriend.GetMatches();
            this.Matches = matches.Count;
        }
    }
}

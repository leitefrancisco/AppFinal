using System;
using System.Collections.Generic;
using System.Text;
using AppFinal.Cash;

namespace AppFinal.ViewModels
{
    internal class FriendProfileViewModel : BaseViewModel
    {
        public string UserName { get; } = CurrentUser.GetUser().username;
        public string UserPicture { get; } = CurrentUser.GetUser().pictureUrl;
        public int FriendsAmt { get; } = CurrentUser.GetUser().friends.Count;
        public int Matches { get; set; }
        public string UserRegion { get; } = CurrentUser.GetUser().region;


    }
}

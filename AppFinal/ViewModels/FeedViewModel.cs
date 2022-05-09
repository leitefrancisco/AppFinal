﻿using System;
using System.Windows.Input;
using AppFinal.Cash;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppFinal.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        public string UserPicture { get; } = CurrentUser.GetUser().pictureUrl;
        public FeedViewModel()
        {
            Title = "Feed";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.gamespot.com/news/"));
        }
        public ICommand OpenWebCommand { get; }
    }
}

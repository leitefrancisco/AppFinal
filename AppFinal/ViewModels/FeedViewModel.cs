using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppFinal.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        public FeedViewModel()
        {
            Title = "Feed";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.gamespot.com/news/"));
        }
        public ICommand OpenWebCommand { get; }
    }
}

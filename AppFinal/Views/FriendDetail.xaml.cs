using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFinal.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendDetail : ContentPage
    {
        public FriendDetail()
        {
            InitializeComponent();
            BindingContext = new FriendDetailViewModel();
        }
    }
}
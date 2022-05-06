﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendProfileView : ContentPage
    {
        private string userID;
        public FriendProfileView(string userID)
        {
            InitializeComponent();
            this.userID = userID;
        }
    }
}
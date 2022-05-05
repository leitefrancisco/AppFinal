﻿using System;
using System.Runtime.CompilerServices;
using AppFinal.Cash;
using AppFinal.Views;
using Xamarin.Forms;

namespace AppFinal
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(Feed), typeof(Feed));
            Routing.RegisterRoute(nameof(Games), typeof(Games));
            Routing.RegisterRoute(nameof(Friends), typeof(Friends));
            Routing.RegisterRoute(nameof(Registration), typeof(Registration));
            Routing.RegisterRoute(nameof(Profile),typeof(Profile));
        }

       
        private void CheckLogin()
        {
            if (CurrentUser.GetUser() != null)
            {

            }
        }

    }
}

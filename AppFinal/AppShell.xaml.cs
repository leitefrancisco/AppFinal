using AppFinal.ViewModels;
using AppFinal.Views;
using System;
using System.Collections.Generic;
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
        }

    }
}

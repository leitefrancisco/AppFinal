using AppFinal.Views;
using System;
using AppFinal.DB.AccessClasses;
using AppFinal.DB.Source;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace AppFinal
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<UserDbAccess>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

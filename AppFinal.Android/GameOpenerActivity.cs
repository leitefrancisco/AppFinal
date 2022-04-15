using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFinal.Droid;
using AppFinal.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(GameOpenerActivity))]

namespace AppFinal.Droid
{
    // https://gist.github.com/aybarsyalcin/eb243a2274d80659e81d50bec50e1ce1


    [Activity(Label = "GameOpenerActivity")]
    public class GameOpenerActivity : Activity, IOpenGame
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        public void OpenGame(string game)
        {
            
            Intent intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage(game);

            // If not NULL run the app, if not, take the user to the app store
            if (intent != null)
            {
                intent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent);
            }
            else
            {
                intent = new Intent(Intent.ActionView);
                intent.AddFlags(ActivityFlags.NewTask);
                intent.SetData(Android.Net.Uri.Parse($"market://details?id={game}"));
                Android.App.Application.Context.StartActivity(intent);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace AppFinal.Views
{
    
    public partial class Games : ContentPage
    {
        public Games()
        {
            InitializeComponent();
        }

        private void Open_Game(object sender, EventArgs e)
        {
            //string packageName = "com.DefaultCompany.com.unity.template.mobile2D";
            //OpenAppOrStore(packageName);
            //You need to change your app package address which you want to open with button. Example: "com.microsoft.office.officelens"
            //Intent intent = new Intent();
            //intent.SetComponent(new ComponentName("com.DefaultCompany.com", "com.DefaultCompany.com.unity.template.mobile2D"));
            //Android.App.Application.Context.StartActivity(intent);
            

        }
        //public void OpenAppOrStore(string packageName)
        //{
        //    PackageManager? packageManager = Android.App.Application.Context.PackageManager;
        //    if (packageManager == null)
        //    {
        //        return;
        //    }

        //    if (!_storesMapping.TryGetValue(packageName, out var appPackageName))
        //    {
        //        return;
        //    }

        //    Intent? intent;
        //    var isAppInstalled = IsAppInstalled(appPackageName);
        //    if (isAppInstalled)
        //    {
        //        intent = packageManager.GetLaunchIntentForPackage(appPackageName);
        //    }
        //    else
        //    {
        //        intent = new Intent(Intent.ActionView);
        //        intent.SetData(Uri.Parse($"market://details?id={appPackageName}"));
        //    }

        //    intent!.AddFlags(ActivityFlags.NewTask);

        //    Android.App.Application.Context.StartActivity(intent);
        //}
        //public bool IsAppInstalled(string packageName)
        //{
        //    PackageManager? pm = Android.App.Application.Context.PackageManager;
        //    if (pm == null)
        //    {
        //        return false;
        //    }

        //    bool installed;
        //    try
        //    {
        //        pm.GetPackageInfo(packageName, PackageInfoFlags.Activities);
        //        installed = true;
        //    }
        //    catch (PackageManager.NameNotFoundException)
        //    {
        //        installed = false;
        //    }

        //    return installed;
        //}

        //private readonly Dictionary<string, string> _storesMapping = new Dictionary<string, string>
        //{
        //    {"comgooglemaps://", "com.google.android.apps.maps"}
        //};


    }
    
}
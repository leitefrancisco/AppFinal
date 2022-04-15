using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFinal.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Friends : ContentPage
    {
        public Friends()
        {
            InitializeComponent();
            // UserDBAccess dba = new UserDBAccess();
            // foreach (var v in dba.GetUsers())
            // {
            //     var underlineLabel = new Label { Text = v.ToString(), TextDecorations = TextDecorations.Underline };
            // }
            

        }

    }
}
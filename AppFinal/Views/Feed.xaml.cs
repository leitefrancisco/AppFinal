using System.Threading.Tasks;
using AppFinal.DB.AccessClasses;
using AppFinal.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Feed : ContentPage
    {


        public Feed()
        {
            InitializeComponent();
        }

        private async Task FillPost(Post post)
        {
            var newGrid = new Grid()
            {
                Margin = new Thickness(10,0,10,0),
                BackgroundColor = Color.LightGray,
                Padding = new Thickness(10,10,10,10),
            };
            var rowDef1 = new RowDefinition();
            var rowDef2 = new RowDefinition();
            var rowDef3 = new RowDefinition();

            rowDef1.Height = 80;
            rowDef3.Height = 50;

            newGrid.RowDefinitions.Add(rowDef1);
            newGrid.RowDefinitions.Add(rowDef2);
            newGrid.RowDefinitions.Add(rowDef3);

            

            var user = await new UserDbAccess().FindOne(post.UserId);

            var image = new Image
            {
                Source = (user.pictureUrl),
                Aspect = Aspect.AspectFit,
                HeightRequest = 200,
                WidthRequest = 200,
            };
            Grid.SetRow(image,0);
            Grid.SetColumn(image,0);

        }
        
        // <Label Text = "Seu Juca" Grid.Column="1" Grid.ColumnSpan="2" Grid.Row="0" VerticalTextAlignment="Center" FontSize="20"></Label>
        // <Label Grid.Row ="1" Margin= "25,10,25,10" Grid.ColumnSpan= "3" Text= "Sou novo aqui , mas se usar direito toda rede social é pra transa" ></ Label >
        // < Button Grid.Row= "2" Grid.Column= "0" FontSize= "14" Text= "I like it!!!" BackgroundColor= "#003638" ></ Button >
        // < Button Grid.Row= "2" Grid.Column= "1" FontSize= "14" Text= "Visit Profile" BackgroundColor= "#003638" ></ Button >
        // < Label Grid.Row= "2" Grid.Column= "2" Text= "timeStamp" FontSize= "10" HorizontalTextAlignment= "Center" ></ Label >
        //
        // </ Grid >

    }
}
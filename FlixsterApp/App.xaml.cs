using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlixsterApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           // MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            MainPage = new SplasScreen();
            await Task.Delay(5000);
            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

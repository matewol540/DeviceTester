using System;
using DeviceTester.Content.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: ExportFont("EncodeSansSC-VariableFont_wdth,wght.ttf",Alias ="Sans")]
namespace DeviceTester
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var _MainPage = new MainPage();
            NavigationPage.SetHasBackButton(_MainPage,false);
            NavigationPage.SetHasNavigationBar(_MainPage, false);
            MainPage = new NavigationPage(_MainPage)
            {
                BarTextColor = Color.White
            };
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

using System;
using System.Collections.Generic;
using System.Linq;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DeviceTester.Content.Pages.SystemF
{
    public partial class Networking : ContentPage, IPageWithNotifier
    {
        private Animation _animation;
        public Networking()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Networking", Constants.LoremTemp, this);
            var tempTuple = Constants.System.Find(x => x.Item1.GetType() == typeof(DeviceInfoPageFactory));
            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;
            this.MainGrid.Children.Add(tmpComp, 0, 0);
        }
        public void ChangeDescriptionState(bool State)
        {
            GridLength HeightValue = new GridLength(50, GridUnitType.Absolute);
            switch (State)
            {
                case true:
                    HeightValue = new GridLength(50, GridUnitType.Absolute);
                    break;
                case false:
                    HeightValue = new GridLength(230, GridUnitType.Absolute);
                    break;
            }
            _animation = new Animation(
                        (d) => MainGrid.RowDefinitions[0] = new RowDefinition() { Height = HeightValue });
            _animation.Commit(this, "Device info Animation", 16, 1000000, Easing.BounceIn, null, null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += UpdateConnection;

            AccessLabel.Text = Connectivity.NetworkAccess.ToString();
            ConnectionTypeLabel.Text = Connectivity.ConnectionProfiles.First().ToString();
        }

        private void UpdateConnection(object sender, ConnectivityChangedEventArgs e)
        {
            AccessLabel.Text = e.NetworkAccess.ToString();
            ConnectionTypeLabel.Text = e.ConnectionProfiles.First().ToString();
        }

        void OpenWebView_Clicked(System.Object sender, System.EventArgs e)
        {
            var wv = new WebViewPage();
            NavigationPage.SetHasBackButton(wv, true);
            NavigationPage.SetHasNavigationBar(wv, true);


            Navigation.PushModalAsync(wv);
        }
    }

    public class NetworkingPageFactory : PageFactory
    {
        public override string getPageName() => "Networking";

        public override Page getPageObject() => new Networking();
    }
}

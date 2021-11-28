using System;
using System.Collections.Generic;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.SystemF
{
    public partial class DeviceInfoPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;

        public DeviceInfoPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Device info", Constants.LoremTemp, this);
            var tempTuple = Constants.System.Find(x => x.Item1.GetType() == typeof(DeviceInfoPageFactory));
            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;
            this.MainGrid.Children.Add(tmpComp, 0, 0);

            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.DeviceType.Text = $"Device type: {DeviceInfo.DeviceType}";
            this.Idiom.Text = $"Device: {DeviceInfo.Idiom}";
            this.Manufacturer.Text = $"Manufacturer: {DeviceInfo.Manufacturer}";
            this.Model.Text = $"Model: {DeviceInfo.Model}";
            this.Name.Text = $"Device name: {DeviceInfo.Name}";
            this.Platform.Text = $"OS type: {DeviceInfo.Platform}";
            this.OSVerion.Text = $"OS version: {DeviceInfo.Version}";

            this.BatteryLoad.Text = $"Charge [%]: {Battery.ChargeLevel * 100}";
            this.State.Text = $"State: {Battery.State}";
            this.Source.Text = $"Source: {Battery.PowerSource}";
        }

        void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            BatteryLoad.Text = $"Charge [%]: {e.ChargeLevel*100}";
            State.Text = $"State: {e.State}";
            Source.Text = $"Source: {e.PowerSource}";
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
    }

    public class DeviceInfoPageFactory : PageFactory
    {
        public override string getPageName() => "Device info";
        public override Page getPageObject()
        {
            return new DeviceInfoPage();
        }
    }

}

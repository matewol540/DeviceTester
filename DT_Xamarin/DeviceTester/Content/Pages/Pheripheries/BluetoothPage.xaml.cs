using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Models;
using DeviceTester.Resources;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class BluetoothPage : ContentPage,IPageWithNotifier
    {
        private Animation _animation;
        ObservableCollection<BluetoothDeviceModel> models = new ObservableCollection<BluetoothDeviceModel>();


        public BluetoothPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Bluetooth", Constants.LoremTemp, this);
            var tempTuple = Constants.Pheriphery.Find(x => x.Item1.GetType() == typeof(BarometerPageFactory));
            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            this.MainGrid.Children.Add(tmpComp, 0, 0);
            this.DeviceListView.ItemsSource = models;
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
            _animation = new Xamarin.Forms.Animation(
                        (d) => MainGrid.RowDefinitions[0] = new RowDefinition() { Height = HeightValue });
            _animation.Commit(this, "GPS Animation", 16, 1000000, Easing.BounceIn, null, null);
        }
    }

    public class BluetoothPageFactory : PageFactory
    {
        public override string getPageName() => "Bluetooth";

        public override Page getPageObject()
        {
            return new BluetoothPage();
        }
    }
}

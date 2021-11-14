using System;
using System.Collections.Generic;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class MagnetometerPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;

        public MagnetometerPage()
        {
            InitializeComponent();
            var tmpComp = new ViewTittleLabel("Magnetometer", Constants.LoremTemp, this);

            var tempTuple = Constants.Pheriphery.Find(x => x.Item1.GetType() == typeof(MagnetometerPageFactory));

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
            _animation.Commit(this, "Magnetometer Animation", 16, 1000000, Easing.BounceIn, null, null);
        }
    }
    public class MagnetometerPageFactory : PageFactory
    {
        public override string getPageName() => "Magnetometer";
        public override Page getPageObject()
        {
            return new MagnetometerPage();
        }
    }
}

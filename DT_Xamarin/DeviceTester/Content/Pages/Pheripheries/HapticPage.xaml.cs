using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class HapticPage : ContentPage
    {
        public HapticPage()
        {
            InitializeComponent();
            //var tempTuple = Constants.Pheriphery.Find(x => x.Item1.GetType() == typeof(CompasPageFactory));

            //tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            //tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            //BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            //BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;

        }
    }
    public class HapticPageFactory : PageFactory
    {
        public override string getPageName() => "Haptics";
        public override Page getPageObject()
        {
            return new HapticPage();
        }
    }
}

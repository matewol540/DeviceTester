using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class CompasPage : ContentPage
    {
        public CompasPage()
        {
            InitializeComponent();
            //var tempTuple = Constants.Pheriphery.Find(x => x.Item1.GetType() == typeof(CompasPageFactory));

            //tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            //tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            //BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            //BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;
        }
    }
    public class CompasPageFactory : PageFactory
    {

        public override string getPageName() => "Compass";
        public override Page getPageObject()
        {
            return new CompasPage();
        }
    }
}

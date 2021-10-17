using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class MagnetometerPage : ContentPage
    {
        public MagnetometerPage()
        {
            InitializeComponent();
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

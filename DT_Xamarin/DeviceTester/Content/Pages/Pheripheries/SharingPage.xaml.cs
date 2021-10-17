using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class SharingPage : ContentPage
    {
        public SharingPage()
        {
            InitializeComponent();
        }
    }
    public class SharingPageFactory : PageFactory
    {
        public override string getPageName() => "Sharing";
        public override Page getPageObject()
        {
            return new SharingPage();
        }
    }
}

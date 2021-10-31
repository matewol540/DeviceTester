using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class SocialPage : ContentPage
    {
        public SocialPage()
        {
            InitializeComponent();
        }
    }
    public class SocialPageFactory : PageFactory
    {
        public override string getPageName() => "Authentification";

        public override Page getPageObject()
        {
            return new SocialPage();
        }
    }
}

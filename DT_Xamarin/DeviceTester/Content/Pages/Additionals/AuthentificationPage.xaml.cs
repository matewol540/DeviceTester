using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class AuthentificationPage : ContentPage
    {
        public AuthentificationPage()
        {
            InitializeComponent();
        }
    }
    public class AuthentificationPageFactory : PageFactory
    {
        public override string getPageName() => "Authentification";

        public override Page getPageObject()
        {
            return new AuthentificationPage();
        }
    }
}

using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class ARKitPage : ContentPage
    {
        public ARKitPage()
        {
            InitializeComponent();
        }
    }
    public class ARKitPageFactory : PageFactory
    {
        public override string getPageName() => "Steps";

        public override Page getPageObject()
        {
            return new ARKitPage();
        }
    }

}

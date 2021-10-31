using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class SpeakPage : ContentPage
    {
        public SpeakPage()
        {
            InitializeComponent();
        }
    }
    public class SpeakPageFactory : PageFactory
    {
        public override string getPageName() => "Speak";

        public override Page getPageObject()
        {
            return new SpeakPage();
        }
    }
}

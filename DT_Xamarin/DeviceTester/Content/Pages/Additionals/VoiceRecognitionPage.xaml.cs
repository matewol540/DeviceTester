using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class VoiceRecognitionPage : ContentPage
    {
        public VoiceRecognitionPage()
        {
            InitializeComponent();
        }
    }
    public class VoiceRecognitionPageFactory : PageFactory
    {
        public override string getPageName() => "Voice";

        public override Page getPageObject()
        {
            return new AuthentificationPage();
        }
    }
}

using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class FaceRecognition : ContentPage
    {
        public FaceRecognition()
        {
            InitializeComponent();
        }
    }
    public class FaceRecognitionFactory : PageFactory
    {
        public override string getPageName() => "Authentification";

        public override Page getPageObject()
        {
            return new FaceRecognition();
        }
    }
}

using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class CameraPage : ContentPage
    {
        public CameraPage()
        {
            InitializeComponent();
        }
    }
    public class CameraPageFactory : PageFactory
    {

        public override string getPageName() => "Camera";
        public override Page getPageObject()
        {
            return new CameraPage();
        }
    }
}

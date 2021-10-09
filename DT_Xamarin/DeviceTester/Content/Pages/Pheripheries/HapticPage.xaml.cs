using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class HapticPage : ContentPage
    {
        public HapticPage()
        {
            InitializeComponent();
        }
    }
    public class HapticPageFactory : PageFactory
    {
        public override Page getPageObject()
        {
            return new HapticPage();
        }
    }
}

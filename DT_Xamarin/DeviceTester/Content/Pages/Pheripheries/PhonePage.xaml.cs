using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class PhonePage : ContentPage
    {
        public PhonePage()
        {
            InitializeComponent();
        }
    }
    public class PhonePageFactory : PageFactory
    {
        public override Page getPageObject()
        {
            return new PhonePage();
        }
    }
}

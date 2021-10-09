using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class CompasPage : ContentPage
    {
        public CompasPage()
        {
            InitializeComponent();
        }
    }
    public class CompasPageFactory : PageFactory
    {
        public override Page getPageObject()
        {
            return new CompasPage();
        }
    }
}

using System;

using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public class NFCPage : ContentPage
    {
        public NFCPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}


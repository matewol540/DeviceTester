using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DeviceTester.Content.CustomComponent
{
    public partial class CustomButton : ContentView
    {
        public CustomButton()
        {
            InitializeComponent();
        }

        public async void Button_Clicked(object sender,EventArgs eventArgs)
        {
            await Navigation.PopModalAsync();
        }
    }
}

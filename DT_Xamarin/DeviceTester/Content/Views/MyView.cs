using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DeviceTester.Content.Views
{
    public class MyView : ContentView
    {
        public MyView() => this.SetScene();

        private void SetScene()
        {
            this.Content =new Frame()
            {
                CornerRadius = 10,
                IsClippedToBounds = true,
                Padding = -5,
                Content = getInsideImage()
            };
        }
        private Image getInsideImage()
        {
            var InsideImage = new Image();
            InsideImage.Aspect = Aspect.AspectFill;
            var gr = new TapGestureRecognizer();
            gr.Tapped += OpenView;
            gr.NumberOfTapsRequired = 1;
            InsideImage.GestureRecognizers.Add(gr);
            return InsideImage;
        }


        private async void OpenView(object sender,EventArgs e)
        {
            var Navi = new NavigationPage(new ContentPage());
            NavigationPage.SetHasBackButton(Navi, true);
            await Navigation.PushAsync(Navi);
        }
    }
}


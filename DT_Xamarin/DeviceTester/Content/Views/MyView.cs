using System;
using System.Collections.Generic;
using Xamarin.Forms;
using RedCorners.Forms;

namespace DeviceTester.Content.Views
{
    public class MyView : ContentView2
    {
        private Page page;
        public MyView(Page childPage,Color StartColor,Color StopColor) => this.SetScene(childPage,StartColor,StopColor);

        private void SetScene(Page childPage,Color StartColor,Color StopColor)
        {
            this.Content = new Frame2
            {
                CornerRadius = 30,
                IsClippedToBounds = true,
                Padding = -5,
                Content = getInsideImage(),
                ShadowColor = Color.DarkRed,
                ShadowRadius = 20,
                Background = new LinearGradientBrush {
                    GradientStops = new GradientStopCollection {
                        new GradientStop(StopColor,0.1F),
                        new GradientStop(StartColor,1.0F)}
                }
            };
            this.page = childPage;
        }

        private Image getInsideImage()
        {
            var InsideImage = new Image {
                Source = ImageSource.FromResource("DeviceTester.Resources.Images.Satellite.png"),
            };
            InsideImage.Aspect = Aspect.AspectFill;
            var gr = new TapGestureRecognizer();
            gr.Tapped += OpenView;
            gr.NumberOfTapsRequired = 1;
            InsideImage.GestureRecognizers.Add(gr);
            return InsideImage;
        }


        private async void OpenView(object sender,EventArgs e)
        {
            var Navi = new NavigationPage(this.page);
            NavigationPage.SetHasBackButton(Navi, true);
            await Navigation.PushAsync(Navi);
        }
    }
}


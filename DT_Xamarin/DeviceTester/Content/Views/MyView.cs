using System;
using System.Collections.Generic;
using Xamarin.Forms;
using RedCorners.Forms;
using DeviceTester.Interfaces;

namespace DeviceTester.Content.Views
{
    public class MyView : ContentView2
    {
        private PageFactory page;
        public MyView(PageFactory childPage,Color StartColor,Color StopColor,String ImageSourceString) => this.SetScene(childPage,StartColor,StopColor,ImageSourceString);

        private void SetScene(PageFactory childPage,Color StartColor,Color StopColor,String ImageSourceString)
        {
            this.Content = new Frame2
            {
                CornerRadius = 30,
                IsClippedToBounds = true,
                Padding = -5,
                Content = getInsideImage(ImageSourceString),
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

        private Image getInsideImage(String ImageSourceString)
        {
            try
            {
                Console.Out.WriteLine($"Function enter: {nameof(getInsideImage)} with arg {ImageSourceString}");
                var tmp_ImageSource = ImageSourceString == "" ? ImageSource.FromResource("DeviceTester.Resources.Images.Satellite.png") : ImageSource.FromResource(ImageSourceString);
                var InsideImage = new Image
                {
                    //Source = ImageSource.FromResource()
                    Source = tmp_ImageSource
                };

                InsideImage.Aspect = Aspect.AspectFit;
                InsideImage.Margin = new Thickness(30);
                var gr = new TapGestureRecognizer();
                gr.Tapped += OpenView;
                gr.NumberOfTapsRequired = 1;
                InsideImage.GestureRecognizers.Add(gr);
                return InsideImage;
            } catch (Exception Er)
            {
                Console.Out.WriteLine($"Following exception has been thrown in {nameof(getInsideImage)}: {Er.Message}");
            }
            return null;
        }


        private async void OpenView(object sender,EventArgs e)
        {
            var Navi = new NavigationPage(this.page.getPageObject())
            {
                BarTextColor = Color.White
            };
            NavigationPage.SetHasBackButton(Navi, false);
            NavigationPage.SetHasNavigationBar(Navi, false);
            await Navigation.PushModalAsync(Navi);
        }

    }
}


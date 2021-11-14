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

        private void SetScene(PageFactory childPage, Color StartColor, Color StopColor, string ImageSourceString)
        {

            var tempGrid = new Grid();
            tempGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1,GridUnitType.Star) });
            tempGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(34, GridUnitType.Absolute) });

            tempGrid.Children.Add(getInsideImage(ImageSourceString), 0, 0);
            tempGrid.Children.Add(new Label() {
                Text = childPage?.getPageName(),
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.AntiqueWhite,
                FontFamily = "Sans"
            }, 0, 1);

            this.Content = new Frame2
            {
                CornerRadius = 30,
                IsClippedToBounds = true,
                Padding = -5,
                Content = tempGrid,
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
                var tmp_ImageSource = ImageSourceString == "" ? ImageSource.FromResource("DeviceTester.Resources.Images.MissingFunctionIcon.png") : ImageSource.FromResource(ImageSourceString);
                var InsideImage = new Image
                {
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


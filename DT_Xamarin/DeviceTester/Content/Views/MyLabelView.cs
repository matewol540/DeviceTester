using System;
using RedCorners.Forms;
using Xamarin.Forms;

namespace DeviceTester.Content.Views
{
    public class MyLabelView : ContentView
    {
        public MyLabelView(String TittleText)
        {
            this.Content = new Frame2
            {
                CornerRadius = 10,
                IsClippedToBounds = true,
                Padding = -5,
                Content = new Label
                {
                    Text = TittleText,
                    FontSize = 35,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontFamily = "Sans",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.Honeydew

                },
                Background = new LinearGradientBrush
                {
                    GradientStops = new GradientStopCollection {
                        new GradientStop(Color.DarkBlue,0.1F),
                        new GradientStop(Color.DarkViolet,1.0F)}
                }
            };
        }
    }
}


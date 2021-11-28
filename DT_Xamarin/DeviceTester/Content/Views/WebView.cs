using System;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DeviceTester.Content.Views
{
    public class WebViewPage : ContentPage
    {
        public WebViewPage()
        {
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);

            WebView webView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = "https://blog.xamarin.com/",
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin=0
            };

            this.Padding = new Thickness(0, 0, 0, 0);

            var BackButton = new Button()
            {
                Text = "Back",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontFamily = "Sans"
            };
            var RefreshButton = new Button()
            {
                Text = "Refresh",
                HorizontalOptions = LayoutOptions.Center,
                FontFamily = "Sans"
            };
            var NextButton = new Button()
            {
                Text = "Next",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                FontFamily = "Sans"

            };

            BackButton.Clicked += (sender, ea) =>
            {
                if (webView.CanGoBack) webView.GoBack();
            };
            RefreshButton.Clicked += (sender, ea) =>
            {
                webView.Reload();
            };
            NextButton.Clicked += (sender, ea) =>
            {
                if (webView.CanGoForward) webView.GoForward();
            };

            this.Content = new StackLayout
            {
                Padding = 0,
                Children =
                {
                    webView,
                    new Frame
                    {
                        CornerRadius = 5,
                        Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding=10,
                        Children =
                        {
                            BackButton,
                            RefreshButton,
                            NextButton
                        }
                    },
                        Padding = 5
                    },
                    
                }
            };
        }
    }
}


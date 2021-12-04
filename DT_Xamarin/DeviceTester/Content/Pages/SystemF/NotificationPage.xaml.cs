using System;
using System.Collections.Generic;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.SystemF
{
    public partial class NotificationPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;
        INotification notificationManager;

        private String _Tittle;
        public String Tittle { get => _Tittle; set
            {
                this._Tittle = value;
                OnPropertyChanged(nameof(Tittle));
            }
        }

        private String _Message;
        public String Message
        {
            get => _Message; set
            {
                this._Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private int _Delay;
        public int Delay { get => _Delay;set
            {
                this._Delay = value;
                OnPropertyChanged(nameof(Delay));
            }
        }

        public NotificationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Notification", Constants.LoremTemp, this);
            var tempTuple = Constants.System.Find(x => x.Item1.GetType() == typeof(DeviceInfoPageFactory));
            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;
            this.MainGrid.Children.Add(tmpComp, 0, 0);

            notificationManager = DependencyService.Get<INotification>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
            notificationManager.Initialize();

        }
        public void ChangeDescriptionState(bool State)
        {
            GridLength HeightValue = new GridLength(50, GridUnitType.Absolute);
            switch (State)
            {
                case true:
                    HeightValue = new GridLength(50, GridUnitType.Absolute);
                    break;
                case false:
                    HeightValue = new GridLength(230, GridUnitType.Absolute);
                    break;
            }
            _animation = new Animation(
                        (d) => MainGrid.RowDefinitions[0] = new RowDefinition() { Height = HeightValue });
            _animation.Commit(this, "Notification Animation", 16, 1000000, Easing.BounceIn, null, null);
        }


        void OnSendClick(object sender, EventArgs e)
        {
            if (Delay == 0)
                notificationManager.SendNotification(Tittle, Message);
            else
                notificationManager.SendNotification(Tittle, Message, DateTime.Now.AddSeconds(Delay));
        }

        void OnSendRemoteClick(object sender,EventArgs e)
        {
            throw new NotImplementedException();
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var frame = new Frame()
                {
                    CornerRadius = 10,
                    BackgroundColor = Color.FromHex("#333333"),
                    Content = new Label()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        TextColor = Color.White,
                        Text = $"Title: {title}\nMessage: {message}"
                    }
                };
                stackLayout.Children.Add(frame);
            });
        }
    }

    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class NotificationPageFactory : PageFactory
    {
        public override string getPageName() => "Notification";

        public override Page getPageObject() => new NotificationPage();
    }
}

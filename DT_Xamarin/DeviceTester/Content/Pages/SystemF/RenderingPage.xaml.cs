using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Helper;
using DeviceTester.Interfaces;
using DeviceTester.Models;
using DeviceTester.Resources;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.SystemF
{

    public partial class RenderingPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;
        private FirebaseConnector fbClient;

        public String timeElapsed { get => _timeElapsed; set
            {
                this._timeElapsed = value;
                OnPropertyChanged(nameof(timeElapsed));
            }
        }
        private String _timeElapsed;
        ObservableCollection<UserModel> Users = new ObservableCollection<UserModel>();


        public RenderingPage()
        {
            InitializeComponent();
            fbClient = new FirebaseConnector();

            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Networking", Constants.Networking, this);
            var tempTuple = Constants.Functions.Find(x => x.Item1.GetType() == typeof(RenderingPageFactory));
            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;
            this.MainGrid.Children.Add(tmpComp, 0, 0);
            this.UsersList.ItemsSource = Users;
            Count.Value = 1;
            Count.Maximum = 1000;
            Count.Minimum = 1;
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
            _animation.Commit(this, "Device info Animation", 16, 1000000, Easing.BounceIn, null, null);
        }
        async void Button_Clicked(Object sender, EventArgs e)
        {
            Users.Clear();
            var result = await fbClient.GetUsersAsync(int.Parse(Count.Value.ToString()));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Users = new ObservableCollection<UserModel>(result);
            this.UsersList.ItemsSource = Users;
            stopwatch.Stop();
            timeElapsed = stopwatch.ElapsedMilliseconds.ToString();
            Console.WriteLine($"Elapsed time is {stopwatch.ElapsedMilliseconds} ms for {Count.Value} elements.");
        }

        void Count_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e) => Count.Value = Math.Round(e.NewValue);

        void UsersList_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            Task.Delay(500);
            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }

    public class RenderingPageFactory : PageFactory
    {
        public override string getPageName() => "API connection";

        public override Page getPageObject() => new RenderingPage();
    }
}

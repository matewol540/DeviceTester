using System;
using System.Collections.Generic;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Forms;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class AuthentificationPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;

        private String _authResult = "";
        public String authResult
        {
            get => _authResult; set
            {
                this._authResult = value;
                OnPropertyChanged(nameof(authResult));
            }
        }

        private String _authState = "";
        public String authState
        {
            get => _authState; set
            {
                this._authState = value;
                OnPropertyChanged(nameof(authState));
            }
        }

        private String _avaible_Auth_Methods = "";
        public String avaible_Auth_Methods
        {
            get => _avaible_Auth_Methods; set
            {
                this._avaible_Auth_Methods = value;
                OnPropertyChanged(nameof(avaible_Auth_Methods));
            }
        }

        private bool _authEnabled = false;
        public bool authEnabled
        {
            get => _authEnabled;
            set
            {
                this._authEnabled = value;
                OnPropertyChanged(nameof(authEnabled));
            }
        }

        public AuthentificationPage()
        {
            InitializeComponent();
            var tmpComp = new ViewTittleLabel("Authentification", Constants.LoremTemp, this);

            var tempTuple = Constants.System.Find(x => x.Item1.GetType() == typeof(AuthentificationPageFactory));

            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;

            this.MainGrid.Children.Add(tmpComp, 0, 0);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            authEnabled = await CrossFingerprint.Current.IsAvailableAsync();
            authState = authEnabled?"Enabled":"Disabled";
            avaible_Auth_Methods = (await CrossFingerprint.Current.GetAuthenticationTypeAsync()).ToString();
            authResult = "Not performed";
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
                        (d) => MainGrid.RowDefinitions[0].Height = HeightValue,0,1,Easing.BounceIn );
            _animation.Commit(this, "GPS Animation", 16, 1000000, Easing.Linear, null, null);
        }

        async void Auth_Clicked(Object sender, EventArgs e)
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Test auth", "Test authentification of user")
            {
                AllowAlternativeAuthentication = true
            });
            authResult = result.Status.ToString();
            await this.Navigation.PushModalAsync(new ResponseModal(result));
        }
    }


    public class AuthentificationPageFactory : PageFactory
    {
        public override string getPageName() => "Authentification";

        public override Page getPageObject()
        {
            return new AuthentificationPage();
        }
    }
}

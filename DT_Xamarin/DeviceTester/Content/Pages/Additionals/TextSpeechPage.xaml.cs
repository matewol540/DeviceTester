using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class TextSpeechPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;
        private ObservableCollection<OwnLocale> _localesColl = new ObservableCollection<OwnLocale>();
        public ObservableCollection<OwnLocale> localesColl { get => _localesColl as ObservableCollection<OwnLocale>; }

        public TextSpeechPage()
        {
            InitializeComponent();
            var tmpComp = new ViewTittleLabel("Text recognition", Constants.LoremTemp, this);

            var tempTuple = Constants.System.Find(x => x.Item1.GetType() == typeof(SharingPageFactory));

            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;
            this.MainGrid.Children.Add(tmpComp, 0, 0);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var tempList = await TextToSpeech.GetLocalesAsync();
            tempList.ForEach<Locale>(x => _localesColl.Add(new OwnLocale(x)));
            LocalePicker.ItemDisplayBinding = new Binding("Country");
            LocalePicker.SelectedIndex = 0;
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
            _animation.Commit(this, "TextSpeech Animation", 16, 1000000, Easing.BounceIn, null, null);
        }

        public async void ReadText_Clicked(Object sender,EventArgs ea)
        {
            var selectedLocal = LocalePicker.SelectedItem as OwnLocale;
            var SpeechOptions = new SpeechOptions
            {
                Volume = (float?)Volume.Value,
                Pitch = (float?)Pitch.Value,
                Locale = selectedLocal.locale
            };
            await TextToSpeech.SpeakAsync(UserText.Text, SpeechOptions);
        }
    }

    public partial class TextSpeechPageFactory : PageFactory
    {
        public override string getPageName() => "Text recognition";
        public override Page getPageObject() => new TextSpeechPage();
    }

    public class OwnLocale
    {
        public Locale locale { get; set; }
        public String Country {get => locale.Language;}
        public OwnLocale(Locale locale)
        {
            this.locale = locale;
        }
    }
}

using System;
using System.Collections.Generic;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class StepsPage : ContentPage, IPageWithNotifier
    {

        ViewTittleLabel LabelTittle;
        private Animation _animation;

        public StepsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            LabelTittle = new ViewTittleLabel("Steps", Constants.LoremTemp, this);
            this.MainGrid.Children.Add(LabelTittle, 0, 0);
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
            _animation.Commit(this, "GPS Animation", 16, 1000000, Easing.BounceIn, null, null);
        }
    }
    public class StepsPageFactory : PageFactory
    {
        public override string getPageName() => "Steps";

        public override Page getPageObject()
        {
            return new StepsPage();
        }
    }
}

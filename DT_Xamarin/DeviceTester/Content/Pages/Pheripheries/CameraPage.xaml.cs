using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;
using System.CodeDom.Compiler;
using System.Linq;
using DeviceTester.Content.Views;
using DeviceTester.Resources;
using Plugin.Media;
using System.Collections.ObjectModel;
using Plugin.Media.Abstractions;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class CameraPage : ContentPage, IPageWithNotifier
    {
        Animation _animation;
        public Grid MainGrid { get => _MainGrid; }
        public CameraPage()
        {
            InitializeComponent();
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
            _animation = new Xamarin.Forms.Animation(
                        (d) => MainGrid.RowDefinitions[0] = new RowDefinition() { Height = HeightValue });
            _animation.Commit(this, "Barometer Animation", 16, 1000000, Easing.BounceIn, null, null);
        }
    }
    public class CameraPageFactory : PageFactory
    {

        public override string getPageName() => "Camera";
        public override Page getPageObject()
        {
            return new CameraPage();
        }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeviceTester.Content.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private Animation _animation;
        public Dictionary<MyView,RowDefinition> FeatureViews = new Dictionary<MyView,RowDefinition>();
        public MainPage()
        {
            InitializeComponent();
            _animation = new Animation();
            FeatureViews = LoadFeatures();
            DisplayList(FeatureViews);
        }

        private Dictionary<MyView, RowDefinition> LoadFeatures()
        {
            var tempDictionary = new Dictionary<MyView, RowDefinition>();
            Constants.GetFunctions.ForEach(function =>
            {
                var functionIndex = Constants.GetFunctions.IndexOf(function);

                var requiredHeight = 200;
                if (functionIndex == Constants.GetFunctions.Count() || functionIndex == 1)
                    requiredHeight = 215;
                var RowDefinition = new RowDefinition { Height = requiredHeight };
                var tempView = new MyView(function.Item1, function.Item2, function.Item3, function.Item4,requiredHeight);

                tempDictionary.Add(tempView, RowDefinition);

                
            });
            return tempDictionary;
        }

        private void DisplayList(Dictionary<MyView, RowDefinition> Views)
        {
            var ViewsToBeDisplayed = Views.Keys.ToList().FindAll(x => x.IsVisible);

            var Child = new Animation((value) =>
            {
                functionGrid_Left.RowDefinitions.Clear();
                functionGrid_Right.RowDefinitions.Clear();
                ViewsToBeDisplayed.ForEach(x =>
                {
                    var functionIndex = ViewsToBeDisplayed.IndexOf(x);
                    var RowDefinition = Views[x];
                    if (functionIndex % 2 == 0)
                    {
                        functionGrid_Left.RowDefinitions.Add(RowDefinition);
                        functionGrid_Left.Children.Add(x, 0, functionGrid_Left.RowDefinitions.IndexOf(RowDefinition));
                    }
                    else
                    {
                        functionGrid_Right.RowDefinitions.Add(RowDefinition);
                        functionGrid_Right.Children.Add(x, 0, functionGrid_Right.RowDefinitions.IndexOf(RowDefinition));
                    }
                    x.Opacity = 0;
                });
            });
            _animation.Add(0, 1, Child);
            ViewsToBeDisplayed.ForEach(x => _animation.Add(0,1,new Animation(value => x.FadeTo(1, 1000))));
            _animation.Commit(this, "SearchAnimation",8,1000,Easing.Linear);
        }

        void SearchBox_TextChanged(Object sender, TextChangedEventArgs e)
        {
            var CurrentVisibleCount = 0;
            if (!String.IsNullOrEmpty(e.NewTextValue))
                CurrentVisibleCount = FeatureViews.Keys.Count(x => x.IsVisible);
            FeatureViews.Keys.ToList().ForEach(x =>
            {
                x.IsVisible = x.page.getPageName().StartsWith(e.NewTextValue);
            });
            if (CurrentVisibleCount != FeatureViews.Keys.Count(x => x.IsVisible))
                DisplayList(FeatureViews);
        }
    }
}

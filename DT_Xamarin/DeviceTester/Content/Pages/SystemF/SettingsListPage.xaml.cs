﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DeviceTester.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeviceTester.Content.Pages.SystemF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsListPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public SettingsListPage()
        {
            InitializeComponent();
            Title = "Settings";
            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
    public class SettingsPageFactory : PageFactory
    {
        public override string getPageName() => "Settings";
        public override Page getPageObject()
        {
            return new SettingsListPage();
        }
    }
}

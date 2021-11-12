using System;
using System.Collections.Generic;
using DeviceTester.Interfaces;
using Xamarin.Forms;

namespace DeviceTester.Content.Views
{
    public partial class ViewTittleLabel : ContentView
    {
        public ContentPage parent;

        private String _LabelTittle;
        public String LabelTitle { get => _LabelTittle; set
            {
                _LabelTittle = value;
                OnPropertyChanged(nameof(LabelTitle));
            }
        }
        private String OldDescription;
        public String _Description;
        public String Description
        {
            get => _Description; set
            {
                _Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private Boolean _ShowDescription;
        public Boolean ShowDescription
        {
            get => _ShowDescription; set
            {
                _ShowDescription = value;
                OnPropertyChanged(nameof(ShowDescription));
            }
        }

        public LinearGradientBrush LineraGradientBck
        {
            get => BackgroundGradient;
        }

        public ViewTittleLabel(string TittleText, String Description,ContentPage parent)
        {
            this.ShowDescription = false;
            this.LabelTitle = TittleText;
            this.Description = Description;
            this.parent = parent;
            OldDescription = Description;
            InitializeComponent();
        }

        public void ChangeVisibilityOfDescription(object sender,EventArgs e)
        {
            (this.parent as IPageWithNotifier)?.ChangeDescriptionState(ShowDescription);
            if (this.ShowDescription)
                this.Description = "";
            else
                this.Description = OldDescription;
            this.ShowDescription = !this.ShowDescription;
        }
    }
}

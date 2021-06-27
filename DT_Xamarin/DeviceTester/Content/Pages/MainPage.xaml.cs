using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeviceTester.Content.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void ChangeToDarkMode(object sender,EventArgs e)
        {
            
            this.BackgroundColor = (this.BackgroundColor == Color.Black ? Color.White:Color.Black) ;
            ((Button)sender).BackgroundColor = (((Button)sender).BackgroundColor == Color.Black ? Color.White:Color.Black) ;
            ((Button)sender).BorderColor = (((Button)sender).BorderColor == Color.Black ? Color.Black:Color.White) ;
            
        }
    }
}

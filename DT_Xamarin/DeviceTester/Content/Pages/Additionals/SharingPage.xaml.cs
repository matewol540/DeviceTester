using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Additionals
{
    public partial class SharingPage : ContentPage,IPageWithNotifier
    {
        private Animation _animation;
        public String selectedImagePath;

        public SharingPage()
        {
            InitializeComponent();
            var tmpComp = new ViewTittleLabel("Sharing", Constants.LoremTemp, this);

            var tempTuple = Constants.System.Find(x => x.Item1.GetType() == typeof(SharingPageFactory));

            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;
            SelectedImage.Source = ImageSource.FromResource("DeviceTester.Resources.Images.MissingFunctionIcon.png");
            this.MainGrid.Children.Add(tmpComp, 0, 0);
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
            _animation.Commit(this, "Shring Animation", 16, 1000000, Easing.BounceIn, null, null);
        }

        async void SelectImage_Clicked(System.Object sender, System.EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            var selectedStream = await DependencyService.Get<IPhotoPicker>().GetImageStreamAsync();
            if (selectedStream == null)
            {
                await DisplayAlert("Error", "Selected media can not be displayed. Please select image.", "Ok");
            } else
            {
                selectedImagePath = await GetFilePathFromStream(selectedStream);
                SelectedImage.Source = selectedStream != null ? ImageSource.FromFile(selectedImagePath) : ImageSource.FromResource("DeviceTester.Resources.Images.MissingFunctionIcon.png");

            }
            (sender as Button).IsEnabled = true;
        }

        public async Task<string> GetFilePathFromStream(Stream stream)
        {
            byte[] data;
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                if (stream.CanRead)
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                else
                {
                    await DisplayAlert("Error", "Error occured while loading image", "Ok");
                    return "";
                }
                data = ms.ToArray();
            }

            var fn = "tempImage.jpeg";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllBytes(file, data);
            return file;
        }

        async void ShareImage_Clicked(Object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(selectedImagePath))
            {
                await DisplayAlert("Error", "Image has not been selected - Please select image.", "Ok");
                return;
            }
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(selectedImagePath)
            });
        }

    }
    public class SharingPageFactory : PageFactory
    {
        public override string getPageName() => "Sharing";
        public override Page getPageObject()
        {
            return new SharingPage();
        }
    }
}

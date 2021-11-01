using AVFoundation;
using CoreGraphics;
using CustomRenderer;
using CustomRenderer.iOS;
using DeviceTester.Content.CustomComponent;
using DeviceTester.Content.Pages.Pheripheries;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.IO;
using DeviceTester.Helper;

[assembly: ExportRenderer(typeof(CameraPage), typeof(CameraPageRenderer))]
namespace CustomRenderer.iOS
{
    public class CameraPageRenderer : PageRenderer
	{
		AVCaptureSession captureSession;
		AVCaptureDeviceInput captureDeviceInput;
		AVCaptureStillImageOutput stillImageOutput;
		CameraPage CameraBasePage;

		private CameraSettings cs;

		#region Controls
		UIView liveCameraStream;
		ImageButton takePhotoButton;
		ImageButton toggleCameraButton;
		ImageButton toggleFlashButton;
		Button DisplaySettingsButton;
		UIImageView lastPictureBox;
		Grid mainGrid;
		ViewTittleLabel headerLabel;
		CustomButton custButton;
		#endregion

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null && Element != null)
            {
				CameraBasePage = (Element as CameraPage);
				mainGrid = CameraBasePage.MainGrid;

				try
                {
					SetupControls();
                    SetupEventHandlers();
                    SetupUserInterfaceAsync();
                    SetupLiveCameraStream();
                    AuthorizeCameraUse();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"\t\t\tERROR: {ex.Message}");
                }
            }
        }

        private void SetupControls()
        {
			headerLabel = new ViewTittleLabel("Camera", Constants.LoremTemp, CameraBasePage);
			custButton = new CustomButton();
			liveCameraStream = new UIView();
			takePhotoButton = new ImageButton();
			toggleFlashButton = new ImageButton();
			toggleCameraButton = new ImageButton();
			DisplaySettingsButton = new Button() { Text = "More Settings", TextColor = Color.AntiqueWhite };
			lastPictureBox = new UIImageView() { BackgroundColor = UIColor.LightGray };


		}

		protected override void Dispose(bool disposing)
		{
			if (captureDeviceInput != null && captureSession != null)
			{
				captureSession.RemoveInput(captureDeviceInput);
			}

			if (captureDeviceInput != null)
			{
				captureDeviceInput.Dispose();
				captureDeviceInput = null;
			}

			if (captureSession != null)
			{
				captureSession.StopRunning();
				captureSession.Dispose();
				captureSession = null;
			}

			if (stillImageOutput != null)
			{
				stillImageOutput.Dispose();
				stillImageOutput = null;
			}

			base.Dispose(disposing);
		}

		void SetupUserInterfaceAsync()
		{
			try
			{
				var gridNative = Platform.CreateRenderer(mainGrid).NativeView;

				var tempGrid = new Grid()
				{
					RowDefinitions = new RowDefinitionCollection()
					{
						new RowDefinition() {Height = new GridLength(30,GridUnitType.Absolute)},
						new RowDefinition() {Height = new GridLength(120,GridUnitType.Absolute)}
					},
					ColumnDefinitions = new ColumnDefinitionCollection()
                    {
						new ColumnDefinition() {Width = new GridLength(30,GridUnitType.Absolute) },
						new ColumnDefinition(),
						new ColumnDefinition() {Width = new GridLength(30,GridUnitType.Absolute) },
						new ColumnDefinition(),
						new ColumnDefinition() {Width = new GridLength(30,GridUnitType.Absolute) }
					}
				};


				//Header Lable
                mainGrid.Children.Add(headerLabel);
                Grid.SetColumnSpan(headerLabel, 5);

				//Back button
                mainGrid.Children.Add(custButton, 0, 4);
                Grid.SetColumnSpan(custButton, 5);

				//Photo control
				var liveCameraStreamView = liveCameraStream.ToView();
				this.mainGrid.Children.Add(liveCameraStreamView, 0, 2);
				Grid.SetColumnSpan(liveCameraStreamView, 5);

				//Take photo button
				takePhotoButton.Source = ImageSource.FromFile("TakePhotoButton.png");
				this.mainGrid.Children.Add(takePhotoButton, 2, 3);


				//Turn on/off flashlight button
				toggleFlashButton.Source = ImageSource.FromFile("NoFlashButton.png");
				tempGrid.Children.Add(toggleFlashButton, 0, 0);

				//Change camera button
				toggleCameraButton.Source = ImageSource.FromFile("ToggleCameraButton.png");
				tempGrid.Children.Add(toggleCameraButton, 4, 0);

				//More Settings Button
				tempGrid.Children.Add(DisplaySettingsButton, 0, 1);
				Grid.SetColumnSpan(DisplaySettingsButton, 4);

				mainGrid.Children.Add(tempGrid, 0, 1);
				Grid.SetColumnSpan(tempGrid, 4);

				//Last picture box
				var lastPictureBoxView = lastPictureBox.ToView();
				this.mainGrid.Children.Add(lastPictureBoxView, 0,3);

				View.Add(gridNative);
			}
			catch (Exception err)
            {
				Console.Out.WriteLine(err.Message);
            }
		}

		void SetupEventHandlers()
		{
			takePhotoButton.Clicked += (object sender, EventArgs e) => {
				CapturePhoto();
			};

			toggleCameraButton.Clicked += (object sender, EventArgs e) => {
				ToggleFrontBackCamera();
			};

			toggleFlashButton.Clicked += (object sender, EventArgs e) => {
				ToggleFlash();
			};
			DisplaySettingsButton.Clicked += (object sender, EventArgs e) =>
			{
				CameraBasePage.Navigation.PushModalAsync(new Camera_Settings(cs));
			};
		}

		async void CapturePhoto()
		{
			var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
			var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);
			var jpegImage = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);

			var photo = new UIImage(jpegImage);
			photo.SaveToPhotosAlbum((image, error) =>
			{
				if (!string.IsNullOrEmpty(error?.LocalizedDescription))
				{
					Console.Error.WriteLine($"\t\t\tError: {error.LocalizedDescription}");
				}
			});
			lastPictureBox.Image = photo;
		}

		void ToggleFrontBackCamera()
		{
			var devicePosition = captureDeviceInput.Device.Position;
			if (devicePosition == AVCaptureDevicePosition.Front)
			{
				devicePosition = AVCaptureDevicePosition.Back;
			}
			else
			{
				devicePosition = AVCaptureDevicePosition.Front;
			}

			var device = GetCameraForOrientation(devicePosition);
			ConfigureCameraForDevice(device);

			captureSession.BeginConfiguration();
			captureSession.RemoveInput(captureDeviceInput);
			captureDeviceInput = AVCaptureDeviceInput.FromDevice(device);
			captureSession.AddInput(captureDeviceInput);
			captureSession.CommitConfiguration();
		}

		void ToggleFlash()
		{
			var device = captureDeviceInput.Device;

			var error = new NSError();
			if (device.HasFlash)
			{
				if (device.FlashMode == AVCaptureFlashMode.On)
				{
					device.LockForConfiguration(out error);
					device.FlashMode = AVCaptureFlashMode.Off;
					device.UnlockForConfiguration();
					toggleFlashButton.Source = ImageSource.FromFile("NoFlashButton.png");
				}
				else
				{
					device.LockForConfiguration(out error);
					device.FlashMode = AVCaptureFlashMode.On;
					device.UnlockForConfiguration();
					toggleFlashButton.Source = ImageSource.FromFile("FlashButton.png");
				}
			}
		}

		AVCaptureDevice GetCameraForOrientation(AVCaptureDevicePosition orientation)
		{
			var devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);

			foreach (var device in devices)
			{
				if (device.Position == orientation)
				{
					return device;
				}
			}
			return null;
		}

		void SetupLiveCameraStream()
		{
			captureSession = new AVCaptureSession();

			var viewLayer = liveCameraStream.Layer;
			var videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
			{
				Frame = new CGRect(0f, 0f,View.Bounds.Width, View.Bounds.Height - 500)
			};
			liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

			var captureDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);
			ConfigureCameraForDevice(captureDevice);
			captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);

			var dictionary = new NSMutableDictionary();
			dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
			stillImageOutput = new AVCaptureStillImageOutput()
			{
				OutputSettings = new NSDictionary()
			};

			captureSession.AddOutput(stillImageOutput);
			captureSession.AddInput(captureDeviceInput);
			captureSession.StartRunning();
		}

		void ConfigureCameraForDevice(AVCaptureDevice device)
		{
			var error = new NSError();
			if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
			{
				device.LockForConfiguration(out error);
				device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
				device.UnlockForConfiguration();
			}
			else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
			{
				device.LockForConfiguration(out error);
				device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
				device.UnlockForConfiguration();
			}
			else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
			{
				device.LockForConfiguration(out error);
				device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
				device.UnlockForConfiguration();
			}
		}

		async void AuthorizeCameraUse()
		{
			var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			if (authorizationStatus != AVAuthorizationStatus.Authorized)
			{
				await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
			}
		}
    }
}
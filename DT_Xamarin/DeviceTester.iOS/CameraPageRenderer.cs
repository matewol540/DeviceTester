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
using DeviceTester.iOS;
using System.Timers;
using CoreMedia;

[assembly: ExportRenderer(typeof(CameraPage), typeof(CameraPageRenderer))]
[assembly: Dependency(typeof(CameraPageRenderer))]
namespace CustomRenderer.iOS
{
    public class CameraPageRenderer : PageRenderer,ICameraService
	{ 
		public CameraPage CameraBasePage { get; set; }

		#region Controls
		UIView liveCameraStream;
		ImageButton takePhotoButton;
		ImageButton toggleCameraButton;
		ImageButton toggleFlashButton;
		ImageButton DisplaySettingsButton;
		UIImageView lastPictureBox;
		Grid mainGrid;
		ViewTittleLabel headerLabel;
		CustomButton custButton;
		#endregion

		#region Computed Properties
		public AppDelegate ThisApp
		{
			get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
		}

        public double DurationMinValue { get => ThisApp.captureDevice.ActiveFormat.MinExposureDuration.Seconds; }
        public double DurationMaxValue { get => ThisApp.captureDevice.ActiveFormat.MaxExposureDuration.Seconds; }
        public double ISOMinValue { get => ThisApp.captureDevice.ActiveFormat.MinISO;  }
        public double ISOMaxValue { get => ThisApp.captureDevice.ActiveFormat.MaxISO; }
        public double BiasMinValue { get => ThisApp.captureDevice.MinExposureTargetBias; }
        public double BiasMaxValue { get => ThisApp.captureDevice.MaxExposureTargetBias; }
        #endregion

        #region Private Variables
        private NSError Error;
		#endregion

		protected override void Dispose(bool disposing)
		{
			if (ThisApp.captureDeviceInput != null && ThisApp.captureSession != null)
			{
				ThisApp.captureSession.RemoveInput(ThisApp.captureDeviceInput);
			}

			if (ThisApp.captureDeviceInput != null)
			{
				ThisApp.captureDeviceInput.Dispose();
				ThisApp.captureDeviceInput = null;
			}

			if (ThisApp.captureSession != null)
			{
				ThisApp.captureSession.StopRunning();
				ThisApp.captureSession.Dispose();
				ThisApp.captureSession = null;
			}

			if (ThisApp.stillImageOutput != null)
			{
				ThisApp.stillImageOutput.Dispose();
				ThisApp.stillImageOutput = null;
			}

			base.Dispose(disposing);
		}

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
			headerLabel = new ViewTittleLabel("Camera", Constants.Camera, CameraBasePage);
			custButton = new CustomButton();
			liveCameraStream = new UIView();
			takePhotoButton = new ImageButton();
			toggleFlashButton = new ImageButton();
			toggleCameraButton = new ImageButton();
			DisplaySettingsButton = new ImageButton();
			lastPictureBox = new UIImageView() { BackgroundColor = UIColor.FromRGBA(0,0,0,1) };
			ThisApp.cameraSettingsModal = new Camera_Settings();
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
						new RowDefinition() {Height = new GridLength(70,GridUnitType.Absolute)},
					},
					ColumnDefinitions = new ColumnDefinitionCollection()
                    {
						new ColumnDefinition() {Width = new GridLength(70,GridUnitType.Absolute) },
						new ColumnDefinition(),
						new ColumnDefinition() {Width = new GridLength(70,GridUnitType.Absolute) }
					}
				};
				


				//Header Lable
				mainGrid.Children.Add(headerLabel);
                Grid.SetColumnSpan(headerLabel, 5);


				//More Settings Button
				DisplaySettingsButton.Source = ImageSource.FromFile("SettingIcon.png");
				tempGrid.Children.Add(DisplaySettingsButton, 2, 0);
				//Turn on/off flashlight button
				toggleFlashButton.Source = ImageSource.FromFile("NoFlashButton.png");
				tempGrid.Children.Add(toggleFlashButton, 0, 0);
				//Change camera button
				toggleCameraButton.Source = ImageSource.FromFile("ToggleCameraButton.png");
				tempGrid.Children.Add(toggleCameraButton, 4, 0);

				mainGrid.Children.Add(tempGrid, 0, 1);
				Grid.SetColumnSpan(tempGrid, 5);


				//Photo control
				liveCameraStream.Layer.CornerRadius = 10;
				liveCameraStream.Layer.MasksToBounds = true;
				var liveCameraStreamView = liveCameraStream.ToView();
				this.mainGrid.Children.Add(liveCameraStreamView, 0, 2);
				Grid.SetColumnSpan(liveCameraStreamView, 5);

				//Last picture box
				var lastPictureBoxView = lastPictureBox.ToView();
				this.mainGrid.Children.Add(lastPictureBoxView, 0,3);

				//Take photo button
				takePhotoButton.Source = ImageSource.FromFile("TakePhotoButton.png");
				this.mainGrid.Children.Add(takePhotoButton, 2, 3);
				

				//Back button
				mainGrid.Children.Add(custButton, 0, 4);
				Grid.SetColumnSpan(custButton, 5);

				var tempTuple = Constants.Functions.Find(x => x.Item1.GetType() == typeof(CameraPageFactory));

				headerLabel.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
				headerLabel.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
				custButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
				custButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;


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
			DisplaySettingsButton.Clicked += async (object sender, EventArgs e) =>
			{
				switch (ThisApp.cameraSettingsModal.focusTypes)
				{
					case FocusTypes.Auto:
						if (!ThisApp.captureDevice.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
						{
							var okCancelAlertController = UIAlertController.Create("Feature not supported", "Sorry, but selected camera does not support custom focus", UIAlertControllerStyle.Alert);
							okCancelAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, alert => { }));
							PresentViewController(okCancelAlertController, true, null);
							return;
						}
						break;
					case FocusTypes.Locked:
						if (!ThisApp.captureDevice.IsFocusModeSupported(AVCaptureFocusMode.Locked))
						{
							var okCancelAlertController = UIAlertController.Create("Feature not supported", "Sorry, but selected camera does not support custom focus", UIAlertControllerStyle.Alert);
							okCancelAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, alert => { }));
							PresentViewController(okCancelAlertController, true, null);
							return;
						}
						break;
                }

				
				try
                {
					if (ThisApp.cameraSettingsModal.Parent != null )
						await CameraBasePage.Navigation.PopModalAsync();
					await CameraBasePage.Navigation.PushModalAsync(ThisApp.cameraSettingsModal);
				} catch (Exception err)
                {
					Console.Out.WriteLine($"{nameof(DisplaySettingsButton)} - {err.Message}");
                }
			};
		}

		async void CapturePhoto()
		{
			var videoConnection = ThisApp.stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
			var sampleBuffer = await ThisApp.stillImageOutput.CaptureStillImageTaskAsync(videoConnection);
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
			lastPictureBox.ContentMode = UIViewContentMode.ScaleAspectFit;
		}

		void ToggleFrontBackCamera()
		{
			var devicePosition = ThisApp.captureDeviceInput.Device.Position;
			if (devicePosition == AVCaptureDevicePosition.Front)
			{
				devicePosition = AVCaptureDevicePosition.Back;
			}
			else
			{
				devicePosition = AVCaptureDevicePosition.Front;
			}

			GetCameraForOrientation(devicePosition);
			ConfigureCameraForDevice();

			ThisApp.captureSession.BeginConfiguration();
			ThisApp.captureSession.RemoveInput(ThisApp.captureDeviceInput);
			ThisApp.captureDeviceInput = AVCaptureDeviceInput.FromDevice(ThisApp.captureDevice);
			ThisApp.captureSession.AddInput(ThisApp.captureDeviceInput);
			ThisApp.captureSession.CommitConfiguration();
		}

		void ToggleFlash()
		{
			var device = ThisApp.captureDeviceInput.Device;

			if (device.HasFlash)
			{
				if (device.FlashMode == AVCaptureFlashMode.On)
				{
					device.LockForConfiguration(out Error);
					device.FlashMode = AVCaptureFlashMode.Off;
					device.UnlockForConfiguration();
					toggleFlashButton.Source = ImageSource.FromFile("NoFlashButton.png");
				}
				else
				{
					device.LockForConfiguration(out Error);
					device.FlashMode = AVCaptureFlashMode.On;
					device.UnlockForConfiguration();
					toggleFlashButton.Source = ImageSource.FromFile("FlashButton.png");
				}
			}
		}

		void GetCameraForOrientation(AVCaptureDevicePosition orientation)
		{
			foreach (var device in AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video))
				if (device.Position == orientation)
					ThisApp.captureDevice =  device;
		}

		void SetupLiveCameraStream()
		{
			ThisApp.captureSession = new AVCaptureSession();
			var videoPreviewLayer = new AVCaptureVideoPreviewLayer(ThisApp.captureSession)
			{
				Frame = new CGRect(-100f, 0f, View.Bounds.Width + 200f, View.Bounds.Height - 390f)
			};


			liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

			ThisApp.captureDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);
			ConfigureCameraForDevice();
			ThisApp.captureDeviceInput = AVCaptureDeviceInput.FromDevice(ThisApp.captureDevice);

			var dictionary = new NSMutableDictionary();
			dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.H264);
			ThisApp.stillImageOutput = new AVCaptureStillImageOutput()
			{
				OutputSettings = new NSDictionary()
			};

			ThisApp.captureSession.AddOutput(ThisApp.stillImageOutput);
			ThisApp.captureSession.AddInput(ThisApp.captureDeviceInput);
			ThisApp.captureSession.StartRunning();
		}

		void ConfigureCameraForDevice()
		{
			if (ThisApp.captureDevice.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
			{
				ThisApp.captureDevice.LockForConfiguration(out Error);
				ThisApp.captureDevice.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
				ThisApp.captureDevice.UnlockForConfiguration();
			}
			else if (ThisApp.captureDevice.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
			{
				ThisApp.captureDevice.LockForConfiguration(out Error);
				ThisApp.captureDevice.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
				ThisApp.captureDevice.UnlockForConfiguration();
			}
			else if (ThisApp.captureDevice.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
			{
				ThisApp.captureDevice.LockForConfiguration(out Error);
				ThisApp.captureDevice.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
				ThisApp.captureDevice.UnlockForConfiguration();
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

		//Focus
        public void FocusMode_ValueChanged()
        {
			ThisApp.captureDevice.LockForConfiguration(out Error);

			// Take action based on the segment selected
			switch (ThisApp.cameraSettingsModal.focusTypes)
			{
				case FocusTypes.Auto:
					// Activate auto focus and start monitoring position
					ThisApp.captureDevice.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
					break;
				case FocusTypes.Locked:
					ThisApp.captureDevice.FocusMode = AVCaptureFocusMode.Locked;
					FocusValue_ValueChanged();
					break;
			}
			// Unlock device
			ThisApp.captureDevice.UnlockForConfiguration();
		}
        public void FocusValue_ValueChanged()
        {
			ThisApp.captureDevice.LockForConfiguration(out Error);
			ThisApp.captureDevice.SetFocusModeLocked((float)(ThisApp.cameraSettingsModal?.FocusValueGet), null);
			ThisApp.captureDevice.UnlockForConfiguration();
		}

    }
}
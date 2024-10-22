using System;
using System.Collections.Generic;
using DeviceTester.Content.Pages;
using DeviceTester.Content.Pages.Additionals;
using DeviceTester.Content.Pages.Pheripheries;
using DeviceTester.Content.Pages.SystemF;
using DeviceTester.Interfaces;
using Xamarin.Forms;


namespace DeviceTester.Resources
{
    public  class Constants
    {
        public delegate Action<Page> CreatePage(Page page);

        public static readonly List<Tuple<PageFactory, Color, Color, String>> Functions = new List<Tuple<PageFactory, Color, Color, String>>() {
            new Tuple<PageFactory,Color,Color,String>(new AccelerometerFactory(),Color.DarkOliveGreen,Color.Lime,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<PageFactory,Color,Color,String>(new BarometerPageFactory(),Color.Indigo,Color.DodgerBlue,"DeviceTester.Resources.Images.Barometer.png"),
            new Tuple<PageFactory,Color,Color,String>(new BluetoothPageFactory(),Color.Indigo,Color.DodgerBlue,"DeviceTester.Resources.Images.Bluetooth.png"),
            new Tuple<PageFactory,Color,Color,String>(new CameraPageFactory(),Color.Sienna,Color.Coral,"DeviceTester.Resources.Images.Camera.png"),
            new Tuple<PageFactory,Color,Color,String>(new GPSPageFactory(),Color.RoyalBlue,Color.LavenderBlush,"DeviceTester.Resources.Images.Satellite.png"),
            new Tuple<PageFactory,Color,Color,String>(new GyroscopePageFactory(),Color.DarkOliveGreen,Color.Lime,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<PageFactory,Color,Color,String>(new HapticPageFactory(),Color.MidnightBlue,Color.SkyBlue,"DeviceTester.Resources.Images.Haptic.png"),
            new Tuple<PageFactory,Color,Color,String>(new MagnetometerPageFactory(),Color.Navy,Color.PowderBlue,"DeviceTester.Resources.Images.Magnetometer.png"),
            new Tuple<PageFactory,Color,Color,String>(new RenderingPageFactory(),Color.Indigo,Color.PowderBlue,"DeviceTester.Resources.Images.APIConnection.png"),
            new Tuple<PageFactory,Color,Color,String>(new AuthentificationPageFactory(),Color.Navy,Color.SkyBlue,"DeviceTester.Resources.Images.Fingerprint2.png"),
            new Tuple<PageFactory,Color,Color,String>(new DeviceInfoPageFactory(),Color.LightSlateGray,Color.DarkSlateGray,"DeviceTester.Resources.Images.Information.png"),
            new Tuple<PageFactory,Color,Color,String>(new NetworkingPageFactory(),Color.FromRgb(19, 66, 12),Color.FromRgb(84, 213, 64),"DeviceTester.Resources.Images.Connection.png"),
            new Tuple<PageFactory,Color,Color,String>(new NotificationPageFactory(),Color.PaleVioletRed,Color.MistyRose,"DeviceTester.Resources.Images.Notification.png"),
            new Tuple<PageFactory,Color,Color,String>(new SharingPageFactory(),Color.DarkViolet,Color.MistyRose,"DeviceTester.Resources.Images.SharingIcon.png"),
            new Tuple<PageFactory,Color,Color,String>(new TextSpeechPageFactory(),Color.SteelBlue,Color.LightSteelBlue,"DeviceTester.Resources.Images.Speaking.png"),
        }; 
        public static List<Tuple<PageFactory, Color, Color, String>> GetFunctions {
            get => Functions;
        }

        public static string Accelerometer = @"The Accelerometer class lets you monitor the device's accelerometer sensor, which indicates the acceleration of the device in three-dimensional space.The Accelerometer functionality works by calling the Start and Stop methods to listen for changes to the acceleration. Any changes are sent back through the ReadingChanged event.";
        public static string Barometer = @"The Barometer class lets you monitor the device's barometer sensor, which measures pressure.The Barometer functionality works by calling the Start and Stop methods to listen for changes to the barometer's pressure reading in hectopascals. Any changes are sent back through the ReadingChanged event.";
        public static string Bluetooth = @"Bluetooth is a short-range wireless technology standard that is used for exchanging data between fixed and mobile devices over short distances using UHF radio waves in the ISM bands, from 2.402 GHz to 2.48 GHz, and building personal area networks (PANs). ";
        public static string Camera = @"he Manual Camera Controls, provided by the AVFoundation Framework in iOS 8, allow a mobile application to take full control over an iOS device's camera. This fine-grained level of control can be used to create professional level camera applications and provide artist compositions by tweaking the parameters of the camera while taking a still image or video.";
        public static string GPS = @"The Map class enables an application to open the installed map application to a specific location or placemark. The Map functionality works by calling the OpenAsync method with the Location or Placemark to open with optional MapLaunchOptions.";
        public static string Gyroscope = @"The Gyroscope class lets you monitor the device's gyroscope sensor which is the rotation around the device's three primary axes. The Gyroscope functionality works by calling the Start and Stop methods to listen for changes to the gyroscope. Any changes are sent back through the ReadingChanged event in rad/s.";
        public static string Haptic = @"The HapticFeedback class lets you control haptic feedback on device. The Haptic Feedback functionality can be performed with a Click or LongPress feedback type.

The Vibration class lets you start and stop the vibrate functionality for a desired amount of time. The Vibration functionality can be requested for a set amount of time or the default of 500 milliseconds.
";
        public static string Magnetometer = @" The Magnetometer class lets you monitor the device's magnetometer sensor which indicates the device's orientation relative to Earth's magnetic field. The Magnetometer functionality works by calling the Start and Stop methods to listen for changes to the magnetometer. Any changes are sent back through the ReadingChanged event. ";
        public static string Rendering = @"Firebase is a platform developed by Google for creating mobile and web applications. It was originally an independent company founded in 2011. In 2014, Google acquired the platform[1] and it is now their flagship offering for app development.";
        public static string Authentification = @"iOS supports two biometric authentication Touch ID and Face ID.

These authentication systems rely on a hardware-based security processor called the Secure Enclave. The Secure Enclave is responsible for encrypting mathematical representations of face and fingerprint data, and authenticating users using this information. According to Apple, face and fingerprint data do not leave the device and are not backed up to iCloud. Apps interact with the Secure Enclave through the Local Authentication API and cannot retrieve face or fingerprint data or directly access the Secure Enclave.";
        public static string DeviceInfo = @"The DeviceInfo class provides information about the device the application is running on.

iOS does not expose an API for developers to get the model of the specific iOS device. Instead a hardware identifier is returned such as iPhone10,6 which refers to the iPhone X.";
        public static string Networking = @"The Connectivity class lets you monitor for changes in the device's network conditions, check the current network access, and how it is currently connected.

It is important to note that it is possible that Internet is reported by NetworkAccess but full access to the web is not available. Due to how connectivity works on each platform it can only guarantee that a connection is available. For instance the device may be connected to a Wi-Fi network, but the router is disconnected from the internet. In this instance Internet may be reported, but an active connection is not available.
";
        public static string Notification = @"New to iOS 10, the User Notification framework allows for the delivery and handling of local and remote notifications. Using this framework, the app or App Extension can schedule the delivery of local notifications by specifying a set of conditions such as location or time of day.

Additionally, the app or extension can receive (and potentially modify) both local and remote notifications as they are delivered to the user's iOS device.

The new User Notification UI framework allows the app or App Extension to customize the appearance of both local and remote notifications when they are presented to the user.";
        public static string Sharing = @"The Share class enables an application to share data such as text and web links to other applications on the device.

The Share functionality works by calling the RequestAsync method with a data request payload that includes information to share to other applications. Text and Uri can be mixed and each platform will handle filtering based on content.";
        public static string TextSpeech = @"The TextToSpeech class enables an application to utilize the built-in text-to-speech engines to speak back text from the device and also to query available languages that the engine can support. Text-to-Speech works by calling the SpeakAsync method with text and optional parameters, and returns after the utterance has finished.

Each platform supports different locales, to speak back text in different languages and accents. Platforms have different codes and ways of specifying the locale, which is why Xamarin.Essentials provides a cross-platform Locale class and a way to query them with GetLocalesAsync.";

        public static String LoremTemp = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tincidunt congue tortor, nec dapibus lectus convallis quis. Nulla lorem neque, aliquam non luctus et, scelerisque quis enim. Quisque vitae turpis ut nulla placerat rutrum eget id orci. Fusce tempor accumsan nisl, ornare dignissim ante ullamcorper sit amet. Sed volutpat justo enim, quis aliquam purus ultrices tincidunt.
Sed scelerisque rutrum neque, quis rutrum lacus convallis id. Aliquam et nunc aliquet, venenatis purus id, eleifend eros. In ac lectus placerat, sagittis urna sed, condimentum lorem. Mauris volutpat sem sem, nec aliquam velit imperdiet quis.
Morbi ac aliquet tellus.Phasellus nec purus sed massa mattis vulputate sit amet eu massa. Phasellus non viverra metus. Nam id orci a est euismod faucibus.Cras viverra erat sit amet mattis fringilla.Pellentesque vitae ex sed nisi posuere laoreet non iaculis nisi. Aliquam at pharetra magna, quis pulvinar sapien.Aenean sem nunc, sodales a ex et, ultrices auctor elit.Nam et nisi at magna accumsan hendrerit.Ut enim nulla, auctor sit amet accumsan eget, congue eu sapien.
Nulla sodales, enim sed dignissim volutpat, urna massa placerat eros, at vestibulum elit erat eu massa. Etiam non odio eget dui commodo cursus.Praesent laoreet, magna eget varius sagittis, mi.";
    }
}

using System;
using System.Collections.Generic;
using DeviceTester.Content.Pages;
using DeviceTester.Content.Pages.Additionals;
using DeviceTester.Content.Pages.Pheripheries;
using DeviceTester.Interfaces;
using Xamarin.Forms;


namespace DeviceTester.Resources
{
    public  class Constants
    {
        public delegate Action<Page> CreatePage(Page page);

        public static readonly List<Tuple<PageFactory, Color, Color, String>> Pheriphery = new List<Tuple<PageFactory, Color, Color, String>>() {
            new Tuple<PageFactory,Color,Color,String>(new AccelerometerFactory(),Color.DarkOliveGreen,Color.Lime,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<PageFactory,Color,Color,String>(new BarometerPageFactory(),Color.Indigo,Color.DodgerBlue,"DeviceTester.Resources.Images.Barometer.png"),
            new Tuple<PageFactory,Color,Color,String>(new BluetoothPageFactory(),Color.Indigo,Color.DodgerBlue,"DeviceTester.Resources.Images.Bluetooth.png"),
            new Tuple<PageFactory,Color,Color,String>(new CameraPageFactory(),Color.Sienna,Color.Coral,"DeviceTester.Resources.Images.Camera.png"),
            new Tuple<PageFactory,Color,Color,String>(new GPSPageFactory(),Color.RoyalBlue,Color.LavenderBlush,"DeviceTester.Resources.Images.Satellite.png"),
            new Tuple<PageFactory,Color,Color,String>(new GyroscopePageFactory(),Color.DarkOliveGreen,Color.Lime,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<PageFactory,Color,Color,String>(new HapticPageFactory(),Color.MidnightBlue,Color.SkyBlue,"DeviceTester.Resources.Images.Haptic.png"),
            new Tuple<PageFactory,Color,Color,String>(new MagnetometerPageFactory(),Color.Navy,Color.PowderBlue,"DeviceTester.Resources.Images.Magnetometer.png"),
        };
        public static readonly List<Tuple<PageFactory, Color, Color, String>> System = new List<Tuple<PageFactory, Color, Color, String>>() {
            new Tuple<PageFactory,Color,Color,String>(new AuthentificationPageFactory(),Color.RoyalBlue,Color.LavenderBlush,"DeviceTester.Resources.Images.Fingerprint2.png"),
            new Tuple<PageFactory,Color,Color,String>(new SettingsPageFactory(),Color.DimGray,Color.Gainsboro,"DeviceTester.Resources.Images.Settings.png"),
            new Tuple<PageFactory,Color,Color,String>(new StepsPageFactory(),Color.DarkViolet,Color.Plum,"DeviceTester.Resources.Images.Steps.png"),
            new Tuple<PageFactory,Color,Color,String>(new SharingPageFactory(),Color.Silver,Color.Silver,""),
            new Tuple<PageFactory,Color,Color,String>(new PhonePageFactory(),Color.Silver,Color.Silver,"")
        };
        public static List<List<Tuple<PageFactory, Color, Color, String>>> GetFunctions { get {
                return new List<List<Tuple<PageFactory, Color, Color, String>>> {
                    Pheriphery,
                    System,
                };
            }
        }


        public static String LoremTemp = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tincidunt congue tortor, nec dapibus lectus convallis quis. Nulla lorem neque, aliquam non luctus et, scelerisque quis enim. Quisque vitae turpis ut nulla placerat rutrum eget id orci. Fusce tempor accumsan nisl, ornare dignissim ante ullamcorper sit amet. Sed volutpat justo enim, quis aliquam purus ultrices tincidunt. Sed scelerisque rutrum neque, quis rutrum lacus convallis id. Aliquam et nunc aliquet, venenatis purus id, eleifend eros. In ac lectus placerat, sagittis urna sed, condimentum lorem. Mauris volutpat sem sem, nec aliquam velit imperdiet quis.

Morbi ac aliquet tellus.Phasellus nec purus sed massa mattis vulputate sit amet eu massa. Phasellus non viverra metus. Nam id orci a est euismod faucibus.Cras viverra erat sit amet mattis fringilla.Pellentesque vitae ex sed nisi posuere laoreet non iaculis nisi. Aliquam at pharetra magna, quis pulvinar sapien.Aenean sem nunc, sodales a ex et, ultrices auctor elit.Nam et nisi at magna accumsan hendrerit.Ut enim nulla, auctor sit amet accumsan eget, congue eu sapien.

Nulla sodales, enim sed dignissim volutpat, urna massa placerat eros, at vestibulum elit erat eu massa. Etiam non odio eget dui commodo cursus.Praesent laoreet, magna eget varius sagittis, mi.";
    }
}

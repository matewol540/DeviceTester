﻿using System;
using System.Collections.Generic;
using DeviceTester.Content.Pages;
using DeviceTester.Content.Pages.Pheripheries;
using DeviceTester.Interfaces;
using Xamarin.Forms;


namespace DeviceTester.Resources
{
    public  class Constants
    {
        public delegate Action<Page> CreatePage(Page page);

        public static readonly List<Tuple<PageFactory, Color, Color, String>> Pheriphery = new List<Tuple<PageFactory, Color, Color, String>>() {
            new Tuple<PageFactory,Color,Color,String>(new AccelerometerFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<PageFactory,Color,Color,String>(new BarometerPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Barometer.png"),
            new Tuple<PageFactory,Color,Color,String>(new CameraPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Camera.png"),
            new Tuple<PageFactory,Color,Color,String>(new CompasPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Compass2.png"),
            new Tuple<PageFactory,Color,Color,String>(new GPSPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Satellite.png"),
            new Tuple<PageFactory,Color,Color,String>(new GyroscopePageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<PageFactory,Color,Color,String>(new HapticPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Haptic.png"),
            new Tuple<PageFactory,Color,Color,String>(new MagnetometerPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Magnetometer.png"),
            new Tuple<PageFactory,Color,Color,String>(new PhonePageFactory(),Color.MediumVioletRed,Color.DodgerBlue,""),
            new Tuple<PageFactory,Color,Color,String>(new SettingsPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Settings.png"),
            new Tuple<PageFactory,Color,Color,String>(new SharingPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"")
        };
        public static readonly List<Tuple<PageFactory, Color, Color, String>> System = new List<Tuple<PageFactory, Color, Color, String>>() {
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,""),
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,""),
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"")
        };
        public static readonly List<Tuple<PageFactory, Color, Color, String>> Additions = new List<Tuple<PageFactory, Color, Color, String>>() {
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,""),
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,""),
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,""),
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"")
        };
        public static readonly List<Tuple<PageFactory, Color, Color, String>> Settings = new List<Tuple<PageFactory, Color, Color, String>>() {
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,""),
            //new Tuple<Page,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"")
        };
        public static List<List<Tuple<PageFactory, Color, Color, String>>> GetFunctions { get {
                return new List<List<Tuple<PageFactory, Color, Color, String>>> {
                    Pheriphery,
                    System,
                    Additions,
                    Settings
                };
            }
        }


        public static String LoremTemp = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tincidunt congue tortor, nec dapibus lectus convallis quis. Nulla lorem neque, aliquam non luctus et, scelerisque quis enim. Quisque vitae turpis ut nulla placerat rutrum eget id orci. Fusce tempor accumsan nisl, ornare dignissim ante ullamcorper sit amet. Sed volutpat justo enim, quis aliquam purus ultrices tincidunt. Sed scelerisque rutrum neque, quis rutrum lacus convallis id. Aliquam et nunc aliquet, venenatis purus id, eleifend eros. In ac lectus placerat, sagittis urna sed, condimentum lorem. Mauris volutpat sem sem, nec aliquam velit imperdiet quis.

Morbi ac aliquet tellus.Phasellus nec purus sed massa mattis vulputate sit amet eu massa. Phasellus non viverra metus. Nam id orci a est euismod faucibus.Cras viverra erat sit amet mattis fringilla.Pellentesque vitae ex sed nisi posuere laoreet non iaculis nisi. Aliquam at pharetra magna, quis pulvinar sapien.Aenean sem nunc, sodales a ex et, ultrices auctor elit.Nam et nisi at magna accumsan hendrerit.Ut enim nulla, auctor sit amet accumsan eget, congue eu sapien.

Nulla sodales, enim sed dignissim volutpat, urna massa placerat eros, at vestibulum elit erat eu massa. Etiam non odio eget dui commodo cursus.Praesent laoreet, magna eget varius sagittis, mi.";
    }
}

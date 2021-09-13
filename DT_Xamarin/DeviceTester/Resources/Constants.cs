using System;
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
            new Tuple<PageFactory,Color,Color,String>(new GyroscopePageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<PageFactory,Color,Color,String>(new AccelerometerFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<PageFactory,Color,Color,String>(new GPSPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Satellite.png"),
            new Tuple<PageFactory,Color,Color,String>(new PeripheryListPageFactory(),Color.MediumVioletRed,Color.DodgerBlue,"")
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
    }
}

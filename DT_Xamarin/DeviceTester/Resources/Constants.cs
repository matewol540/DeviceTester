using System;
using System.Collections.Generic;
using DeviceTester.Content.Pages;
using Xamarin.Forms;


namespace DeviceTester.Resources
{
    public static class Constants
    {

        public static readonly List<Tuple<Page, Color, Color, String>> Pheriphery = new List<Tuple<Page, Color, Color, String>>() {
            new Tuple<Page,Color,Color,String>(new GyroscopePage(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Gyroscope.png"),
            new Tuple<Page,Color,Color,String>(new GyroscopePage(),Color.MediumVioletRed,Color.DodgerBlue,"DeviceTester.Resources.Images.Satellite.png"),
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,""),
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,"")
        };
        public static readonly List<Tuple<Page, Color, Color, String>> System = new List<Tuple<Page, Color, Color, String>>() {
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,""),
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,""),
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,"")
        };
        public static readonly List<Tuple<Page, Color, Color, String>> Additions = new List<Tuple<Page, Color, Color, String>>() {
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,""),
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,""),
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,""),
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,"")
        };
        public static readonly List<Tuple<Page, Color, Color, String>> Settings = new List<Tuple<Page, Color, Color, String>>() {
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,""),
            new Tuple<Page,Color,Color,String>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue,"")
        };
        public static List<List<Tuple<Page, Color, Color, String>>> GetFunctions { get {
                return new List<List<Tuple<Page, Color, Color, String>>> {
                    Pheriphery,
                    System,
                    Additions,
                    Settings
                };
            }
        }
    }
}

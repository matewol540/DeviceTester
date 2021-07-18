using System;
using System.Collections.Generic;
using DeviceTester.Content.Pages;
using Xamarin.Forms;


namespace DeviceTester.Resources
{
    public static class Constants
    {

        public static readonly List<Tuple<Page, Color, Color>> Pheriphery = new List<Tuple<Page, Color, Color>>() {
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue)
        };
        public static readonly List<Tuple<Page, Color, Color>> System = new List<Tuple<Page, Color, Color>>() {
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue)
        };
        public static readonly List<Tuple<Page, Color, Color>> Additions = new List<Tuple<Page, Color, Color>>() {
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue)
        };
        public static readonly List<Tuple<Page, Color, Color>> Settings = new List<Tuple<Page, Color, Color>>() {
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue),
            new Tuple<Page,Color,Color>(new PeripheryListPage(),Color.MediumVioletRed,Color.DodgerBlue)
        };
        public static List<List<Tuple<Page, Color, Color>>> GetFunctions { get {
                return new List<List<Tuple<Page, Color, Color>>> {
                    Pheriphery,
                    System,
                    Additions,
                    Settings
                };
            }
        }
    }
}

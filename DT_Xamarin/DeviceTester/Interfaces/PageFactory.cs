using System;
using Xamarin.Forms;

namespace DeviceTester.Interfaces
{
    public abstract class PageFactory
    {
        public abstract Page getPageObject();

        public abstract String getPageName();
    }
}

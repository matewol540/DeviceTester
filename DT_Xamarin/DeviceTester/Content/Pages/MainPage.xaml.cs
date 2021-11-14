
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeviceTester.Content.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Constants.GetFunctions.ForEach(fuctionGroup =>
                {
                    var functionGroupIndex = Constants.GetFunctions.IndexOf(fuctionGroup);

                    this.MenuGrid.RowSpacing = 20;
                    this.MenuGrid.RowDefinitions.Add(new RowDefinition { Height = 50 });

                    this.MenuGrid.Children.Add(new MenuTittleLabel(getName(fuctionGroup)), 0, functionGroupIndex * 2 ); //Adding title

                    var functionGrid = new Grid() { ColumnSpacing = 20 };
                    var functionGrid_Left = new Grid() { RowSpacing = 20, RowDefinitions = new RowDefinitionCollection() };
                    var functionGrid_Right = new Grid() { RowSpacing = 20, RowDefinitions = new RowDefinitionCollection() };

                    functionGrid.Children.Add(functionGrid_Left, 0, 0);
                    functionGrid.Children.Add(functionGrid_Right, 1, 0);

                    this.MenuGrid.Children.Add(functionGrid, 0, functionGroupIndex * 2 + 1);

                    fuctionGroup.ForEach(TupleItem =>
                    {
                        var functionIndex = fuctionGroup.IndexOf(TupleItem) ;
                        var requiredHeight = 200; // Add here handling to be square shaped
                        if (functionIndex == fuctionGroup.Count())
                            requiredHeight = 215; // Add here handling to be square shaped
                        if (functionIndex == 1)
                            requiredHeight = 215; // Add here handling to be square shaped
                        var RowDefinition = new RowDefinition { Height = requiredHeight };

                        if (functionIndex % 2 == 0)
                        {
                            functionGrid_Left.RowDefinitions.Add(RowDefinition);
                            functionGrid_Left.Children.Add(new MyView(TupleItem.Item1,TupleItem.Item2,TupleItem.Item3,TupleItem.Item4),0,functionGrid_Left.RowDefinitions.IndexOf(RowDefinition));
                        }
                        else
                        {
                            functionGrid_Right.RowDefinitions.Add(RowDefinition);
                            functionGrid_Right.Children.Add(new MyView(TupleItem.Item1, TupleItem.Item2, TupleItem.Item3, TupleItem.Item4), 0,functionGrid_Right.RowDefinitions.IndexOf(RowDefinition));
                        }
                    });
                    this.MenuGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
            );
        }

        private string getName(List<Tuple<PageFactory, Color, Color,String>> fuctionGroup)
        {
            if (fuctionGroup == Constants.Pheriphery)
                return "Pheriphery";
            if (fuctionGroup == Constants.System)
                return "System";
            return "";
        }
    }
}

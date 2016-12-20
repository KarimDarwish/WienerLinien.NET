using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WienerLinienApi.Samples.WPF.Model;

namespace WienerLinienApi.Samples.WPF.View
{
    /// <summary>
    /// Interaction logic for BusStopFavDialog.xaml
    /// </summary>
    public partial class BusStopFavDialog : Window
    {
        public string SelectedLine { get; set; }

        public BusStopFavDialog()
        {
            InitializeComponent();
            
        }

        private async void StopName_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            var lineItems = await NewFavoriteStop.GetLinesFromStation(StopName.Text,"");
            LineName.ItemsSource = lineItems;
        }

        private async void LineName_OnDropDownClosed(object sender, EventArgs e)
        {
            
            var lineItems = await NewFavoriteStop.GetDirections(StopName.Text, LineName.Text, "");
            Direction.ItemsSource = lineItems;
        }
    }
}

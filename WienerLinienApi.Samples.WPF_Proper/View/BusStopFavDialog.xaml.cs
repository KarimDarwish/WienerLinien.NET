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
using WienerLinienApi.Samples.WPF_Proper.Model;

namespace WienerLinienApi.Samples.WPF_Proper.View
{
    /// <summary>
    /// Interaction logic for BusStopFavDialog.xaml
    /// </summary>
    public partial class BusStopFavDialog : Window
    {
        public string SelectedLine { get; set; }
        public BusStopView BSV { get; set; }

        public BusStopFavDialog()
        {
            InitializeComponent();
            //DataContext = new AutocomFile();
            
        }

        private async void StopName_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (!string.IsNullOrEmpty(StopName.Text))
            {
                var lineItems = await NewFavoriteStop.GetLinesFromStation(StopName.Text, "");
                LineName.ItemsSource = lineItems;
            }
        }

        private async void LineName_OnDropDownClosed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StopName.Text) && !string.IsNullOrEmpty(LineName.Text))
            {
                var lineItems = await NewFavoriteStop.GetDirections(StopName.Text, LineName.Text);
                Direction.ItemsSource = lineItems.Values;
            }
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StopName.Text) && !string.IsNullOrEmpty(LineName.Text))
            {
                var x = NewFavoriteStop.GetTimeForNextBus(StopName.Text, LineName.Text, Direction.Text);
                //BSV = new BusStopView(StopName.Text, LineName.Text, );
            }
        }
    }
}

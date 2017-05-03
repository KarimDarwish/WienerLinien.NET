using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class BusStopFavDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string SelectedLine { get; set; }
        public BusStopView BSV { get; set; }


        public string TestText { get; set; }
        private List<string> _testItems;
        public List<string> TestItems {
            get { return _testItems; }
            private set
            {
                _testItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestItems"));
            }
        }

        public List<string> LineNameColl { get; set; }
        public BusStopFavDialog()
        {
            InitializeComponent();
            //DataContext = new AutocomFile();
            Task.Run(async () => { TestItems = await NewFavoriteStop.GetStaionNames("ptBusCity"); }).Wait();

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

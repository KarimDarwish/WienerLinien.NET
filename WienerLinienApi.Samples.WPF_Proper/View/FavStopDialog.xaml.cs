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
using WienerLinienApi.Information;
using WienerLinienApi.Samples.WPF_Proper.Model;

namespace WienerLinienApi.Samples.WPF_Proper.View
{
    /// <summary>
    /// Interaction logic for BusStopFavDialog.xaml
    /// </summary>
    public partial class FavStopDialog : Window, INotifyPropertyChanged
    {
        private MeansOfTransport meameanOfTransport;
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
        public FavStopDialog(MeansOfTransport meanOfTrans)
        {
            meameanOfTransport = meanOfTrans;
            InitializeComponent();
            Task.Run(async () => { TestItems = await NewFavoriteStop.GetStaionNames(meameanOfTransport); }).Wait();
            StopName.ItemsSource = TestItems;           

        }

        private async void StopName_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (!string.IsNullOrEmpty(StopName.Text))
            {
                var lineItems = await NewFavoriteStop.GetLinesFromStation(StopName.Text, meameanOfTransport);
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
               BSV = new BusStopView(StopName.Text, LineName.Text, Direction.Text);

            }
        }

        private void StopName_GotFocus(object sender, RoutedEventArgs e)
        {
            LineName.ItemsSource = null;
            Direction.ItemsSource = null;
        }
    }
}

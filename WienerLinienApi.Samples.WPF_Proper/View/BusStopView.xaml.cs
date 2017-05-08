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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WienerLinienApi.Samples.WPF_Proper.Model;

namespace WienerLinienApi.Samples.WPF_Proper.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BusStopView : UserControl, INotifyPropertyChanged
    {

        public string Stop { get; set; }
        public string Line { get; set; }
        public string Direction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private string _time;

        public string Time
        {
            get { return _time; }
            private set
            {
                _time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Now"));
            }
        }

        public BusStopView(string stop, string line, string direction)
        {
            InitializeComponent();
            this.Stop = stop;
            BusStopNameLabel.Text = Stop;

            Line = line;
            LineName.Text = line;


            Direction = direction;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            timer.Start();

           
        }

        private string getTime(string stop, string line, string direction) {
            return NewFavoriteStop.GetTimeForNextBus(stop, line, direction);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Time = getTime(Stop, Line, Direction);
        }
    }
}

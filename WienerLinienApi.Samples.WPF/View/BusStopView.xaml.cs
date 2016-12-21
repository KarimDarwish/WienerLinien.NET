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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WienerLinienApi.Samples.WPF.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1(string Stop, string Line, string newxtBus)
        {
            InitializeComponent();

            BusStopNameLabel.Text = Stop;
            LineName.Text = Line;
            NextBus.Text = newxtBus;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WienerLinienApi.Samples.WPF_Proper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Object MainContent { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent = Content;
            Storyboard sb = (this.FindResource("LoginAnimation") as Storyboard);
            sb.Begin();
            
        }

        private void Login_Storyboard_Completed(object sender, EventArgs e)
        {
            Content = null;
            Content = new LoginView();
            
        }
    }
}

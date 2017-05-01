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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WienerLinienApi;

namespace WienerLinienApi.Samples.WPF_Proper
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public bool isLogin { get; set; }
        public LoginView()
        {
            isLogin = true;
            InitializeComponent();
            Storyboard sb = (this.FindResource("LoginPrep") as Storyboard);
            sb.Begin();
            
        }

        private void LoginPrep_Storyboard_Completed(object sender, EventArgs e)
        {
            Storyboard sb = (this.FindResource("LoginApear") as Storyboard);
            sb.Begin();
        }

        private void SignUpPrep_Storyboard_Completed(object sender, EventArgs e)
        {
            Storyboard sb = (this.FindResource("SignUpAnimation") as Storyboard);
            sb.Begin();
        }
        
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (isLogin) {
                ToggleMenu();
            }
            else
            {
               
                UserManagement usermanagement = new UserManagement();
               if(usermanagement.Signup(Username.Text, Password.Text, Firstname.Text, Lastname.Text))
                {
                    MessageBox.Show("Signup successful");
                }
               else
                {
                    MessageBox.Show("Signup unsuccessful");
                }
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!isLogin)
            {
                ToggleMenu();
            }
            else
            {
                UserManagement usermanagement = new UserManagement();
                if(usermanagement.Login(Username.Text, Password.Text))
                {
                    MessageBox.Show("Login successful");
                }
                else
                {
                    MessageBox.Show("Login unsuccessful");
                }

            }
  
        }

        private void ToggleMenu() {
            if (isLogin)
            {
                Storyboard sb = (this.FindResource("SignUpPrep") as Storyboard);
                sb.Begin();

            }
            else {
                Storyboard sb = (this.FindResource("LoginPrep") as Storyboard);
                sb.Begin();
            }

            isLogin = !isLogin;
        }
       
    }
}

﻿using System;
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
using WienerLinienApi.Samples.WPF_Proper.View;

namespace WienerLinienApi.Samples.WPF_Proper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainView mV { get; set; }
        public MainWindow()
        {
            mV = new MainView(this);
            InitializeComponent();
            Content = mV;            
        }

        public void changeToLogin() {
            Content = null;
            Content = new LoginView(this);
        }

        public void changeToMain() {
            Content = null;
            Content = mV;
        }

        
    }
}
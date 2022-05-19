using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordMemorize.Helpers;

namespace WordMemorize.Windows
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void ClosePage(object sender, MouseButtonEventArgs e)
        {
            Service.HidePage(this);
        }

        private void MaximizePage(object sender, MouseButtonEventArgs e)
        {
            Service.MaximizePage(this);
        }

        private void MinimizePage(object sender, MouseButtonEventArgs e)
        {
            Service.MinimizePage(this);
        }
    }
}

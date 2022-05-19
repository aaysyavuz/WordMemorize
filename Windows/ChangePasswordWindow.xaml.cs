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
        private void CreateVerCode(){}
        private void SendMail(){}
        private void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            CreateVerCode();
            SendMail();
            verCodetxtBlock.Visibility = Visibility.Visible;
            txtVerificationCode.IsEnabled = true;
            OkButton.IsEnabled = true;
        }

        private bool CheckCode()
        {
            return true;

        }
        private void ChangePassword(){}
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckCode())
            {
                passTxtBlock.Visibility = Visibility.Visible;
                txtPassword.IsEnabled = true;
                changePassBtn.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Your verification code is false!");
            }
        }

        private void ChangePassBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ChangePassword();
        }
    }
}

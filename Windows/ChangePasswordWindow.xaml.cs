using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordMemorize.DatabaseModels;
using WordMemorize.Helpers;

namespace WordMemorize.Windows
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private Service service;
        private int userId;
        private wordmemorizeContext dbContext;
        public ChangePasswordWindow(int userId)
        {
            InitializeComponent();
            service = new Service();
            this.userId = userId;
            dbContext = new wordmemorizeContext();
        }

        private string VerCode;

        private void ClosePage(object sender, MouseButtonEventArgs e)
        {
            service.HidePage(this);
        }

        private void MaximizePage(object sender, MouseButtonEventArgs e)
        {
            service.MaximizePage(this);
        }

        private void MinimizePage(object sender, MouseButtonEventArgs e)
        {
            service.MinimizePage(this);
        }

        private void CreateVerCode()
        {
            VerCode = "1234";
        }
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

        private void ChangePassword()
        {
            User user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            user.Password = txtPassword.Text;
            dbContext.SaveChanges();
        }
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

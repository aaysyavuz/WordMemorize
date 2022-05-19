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
using MaterialDesignThemes.Wpf;
using WordMemorize.DatabaseModels;
using WordMemorize.Helpers;
using WordMemorize.Windows;

namespace WordMemorize
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public wordmemorizeContext databaseContext;
        private Service service;
        public LoginWindow()
        {
            InitializeComponent();
            service = new Service();
            databaseContext = new wordmemorizeContext();
        }
        public string UserEmail;
        public string UserPassword;
        private void Login()
        {
            User user = databaseContext.Users.FirstOrDefault(user => user.Email == UserEmail);
            string password = user.Password;
            if (password == UserPassword)
            {
                Window controlPanel = (user.UserRoleId == 1) ?  (Window) new AdminControlPanel((int)user.Id) : new StudentControlPanel((int)user.Id);
                controlPanel.Show();
            }
            else
            {
                AlertBox alertBox = new AlertBox("Uncorrect pasword!\nPlease try again.");
                alertBox.Show();
            }
        }



        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            service.CloseApp();
        }

        private void MaximizaPage(object sender, MouseButtonEventArgs e)
        {
            service.MaximizePage(this);
        }

        private void MinimizePage(object sender, MouseButtonEventArgs e)
        {
            service.MinimizePage(this);
        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            UserEmail = txtEmail.Text;
            UserPassword = txtPassword.Password;
            if (service.IsRegistered(UserEmail))
            {
                Login();
            }
            else
            {
                AlertBox alertBox = new AlertBox("There isn't any account with this email.Please register first!");
                alertBox.Show();
            }
        }

        private void SignupBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (service.IsRegistered(UserEmail))
            {
                AlertBox alertBox = new AlertBox("You already have an account with this email!");
                alertBox.Show();
            }
            else
            {
                RegisterWindow registerWindow = new RegisterWindow();
                registerWindow.Show();
                this.Hide();
            }
        }

        private void ForgotPassBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (!service.IsRegistered(UserEmail))
            {
                AlertBox alertBox = new AlertBox("There isn't any account with this email.");
                alertBox.Show();
            }
            else
            {
                ChangePasswordWindow changePassWindow = new ChangePasswordWindow((int)databaseContext.Users.FirstOrDefault(u => u.Email == UserEmail).Id);
                changePassWindow.Show();
            }
        }
    }
}

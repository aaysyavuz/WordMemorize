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
        public LoginWindow()
        {
            InitializeComponent();
            databaseContext = new wordmemorizeContext();
        }
        public string UserEmail;
        public string UserPassword;
        private void LoginBtn_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserEmail = txtEmail.Text;
            UserPassword = txtPassword.Password;
            if (Service.IsRegistered(UserEmail)) 
            {
                Login();
            }
            else
            {
                AlertBox alertBox = new AlertBox("There isn't any account with this email.Please register first!");
                alertBox.Show();
            }
        }
        private void Login()
        {
            User user = databaseContext.Users.FirstOrDefault(user => user.Email == UserEmail);
            string password = user.Password;
            if (password == UserPassword)
            {
                Window controlPanel = (user.UserRoleId == 1) ?  (Window) new AdminControlPanel() : new StudentControlPanel();
                controlPanel.Show();
            }
            else
            {
                AlertBox alertBox = new AlertBox("Uncorrect pasword!\nPlease try again.");
                alertBox.Show();
            }
        }

        private void ForgotPassBtn_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Service.IsRegistered(UserEmail))
            {
                AlertBox alertBox = new AlertBox("There isn't any account with this email.");
                alertBox.Show();
            }
            else
            {
                ChangePasswordWindow changePassWindow = new ChangePasswordWindow();
                changePassWindow.Show();
            }
        }

        private void SignupBtn_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Service.IsRegistered(UserEmail))
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

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            Service.CloseApp();
        }

        private void MaximizaPage(object sender, MouseButtonEventArgs e)
        {
            Service.MaximizePage(this);
        }

        private void MinimizePage(object sender, MouseButtonEventArgs e)
        {
            Service.MinimizePage(this);
        }
    }
}

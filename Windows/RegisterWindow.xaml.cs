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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private wordmemorizeContext databaseContext;
        private string Email;
        private string Password;
        private string FirstName;
        private string LastName;
        private enum UserRole
        {
            Teacher = 1,
            Student = 2
        }

        private UserRole role;
        private Service service;
        public RegisterWindow()
        {
            InitializeComponent();
            databaseContext = new wordmemorizeContext();
            service = new Service();
        }

        private void HidePage(object sender, MouseButtonEventArgs e)
        {
            service.HidePage(this);
        }

        private void Maximize(object sender, MouseButtonEventArgs e)
        {
            service.MaximizePage(this);
        }

        private void Minimize(object sender, MouseButtonEventArgs e)
        {
            service.MinimizePage(this);
        }

        private void AdminBtnClick(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(admingCheckBox.IsChecked))
            {
                studentCheckBox.IsChecked = false;
                role = UserRole.Teacher;
            }
        }

        private void StudentBtnClick(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(studentCheckBox.IsChecked))
            {
                admingCheckBox.IsChecked = false;
                role = UserRole.Student;
            }
        }

        private void Signupbtn_OnMouseLeftButtonDown(object sender, RoutedEventArgs routedEventArgs)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
            {
                AlertBox alertBox = new AlertBox("Please fill all fields");
                alertBox.Show();
            }
            else
            {
                Email = txtEmail.Text;
                Password = txtPassword.Text;
                FirstName = txtFirstName.Text;
                LastName = txtLastName.Text;
                if (service.IsRegistered(Email))
                {
                    AlertBox alertBox = new AlertBox("You already have an account with this email!");
                    alertBox.Show();
                }
                else
                {
                    if (role != 0)
                    {
                        CreateAccount();
                        AlertBox alertBox = new AlertBox("You successfully create an account!");
                        alertBox.Show();
                    }
                    else
                    {
                        AlertBox alertBox = new AlertBox("Please select a user role");
                        alertBox.Show();
                    }

                }
            }
            
        }

        private void CreateAccount()
        {
            User newUser = new User();
            newUser.FirstName = FirstName;
            newUser.LastName = LastName;
            newUser.Email = Email;
            newUser.Password = Password;
            newUser.UserRoleId = Convert.ToInt32(role);
            databaseContext.Users.Add(newUser);
            databaseContext.SaveChanges();
        }
    }
}

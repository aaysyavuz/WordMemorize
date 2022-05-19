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
    /// Interaction logic for AdminControlPanel.xaml
    /// </summary>
    public partial class AdminControlPanel : Window
    {
        public AdminControlPanel()
        {
            InitializeComponent();
            FillGrid();
        }

        private string EngWord;
        private string TrWord;
        private DateTime AddedDate;
        private void TextBoxAssign()
        {
            this.EngWord = txtengword.Text;
            this.TrWord = txttrWord.Text;
            this.AddedDate = DateTime.Today;
        }

        private void FillGrid()
        {
            //TODO
        }

        private void AddWord()
        {
            //TODO
        }

        private void CleanTextBoxes()
        {
            txtengword.Clear();
            txttrWord.Clear();
        }
        private void AddQuestionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxAssign();
            AddWord();
            FillGrid();
            CleanTextBoxes();
        }
        private void RemoveWord()
        {
            //TODO
        }
        
        private void RmvQuestionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxAssign();
            RemoveWord();
            FillGrid();
            CleanTextBoxes();
        }

        private void Logoutbtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to log out?", "Warning!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Service.HidePage(this);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }
    }
}

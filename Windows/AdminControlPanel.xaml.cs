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
    /// Interaction logic for AdminControlPanel.xaml
    /// </summary>
    public partial class AdminControlPanel : Window
    {
        public AdminControlPanel(int userId)
        {
            InitializeComponent();
            FillGrid();
            service = new Service();
            dbContext = new wordmemorizeContext();
            this.userId = userId;
        }

        private int userId;
        private Service service;
        private string EngWord;
        private string TrWord;
        private DateTime AddedDate;
        private wordmemorizeContext dbContext;
        private void TextBoxAssign()
        {
            this.EngWord = txtengword.Text;
            this.TrWord = txttrWord.Text;
            this.AddedDate = DateTime.Today;
        }

        private void FillGrid()
        {
            service.FillGrid(words,userId);
        }

        private void AddWord()
        {
            List<User> users = dbContext.Users.ToList();
            Question question = new Question();
            question.EnglishWord = EngWord;
            question.TurkishWord = TrWord;
            question.Level = 0;
            question.Date = DateTime.Today;
            for (int i = 0; i < users.Count; i++)
            {
                question.UserId = users[i].Id;
                dbContext.Questions.Add(question);
            }

            dbContext.SaveChanges();
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
            Question question = dbContext.Questions.FirstOrDefault(q => q.EnglishWord == EngWord && q.TurkishWord == TrWord);
            dbContext.Questions.Remove(question);
            dbContext.SaveChanges();
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
                service.HidePage(this);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }
    }
}

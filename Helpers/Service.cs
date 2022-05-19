using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WordMemorize.DatabaseModels;

namespace WordMemorize.Helpers
{
    public class Service
    {
        private wordmemorizeContext dbContext;
        public Service()
        {
            dbContext = new wordmemorizeContext();
        }
        public bool IsRegistered(string Email)
        {
            wordmemorizeContext databaseContext = new wordmemorizeContext();
            return databaseContext.Users.FirstOrDefault(user => user.Email == Email) != null;
        }

        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        public void HidePage(Window window)
        {
            window.Hide();
        }

        public void MinimizePage(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        public void MaximizePage(Window window)
        {
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
        }
        public void AddWord(){}

        public Setting GetSettings(int userId)
        {
           return dbContext.Settings.Where(s => s.UserId == userId).FirstOrDefault();
        }
        public List<Question> GetQuestions(int userId, bool isPractice)
        {
            List<Question> questions = new List<Question>();
            if (isPractice)
            {
                questions = dbContext.Questions.Where(q => q.UserId == userId && q.Level == 0).ToList();
            }
            else
            {
                Setting settings = GetSettings(userId);
                DateTime level1 = DateTime.Today.AddDays(-settings.Level1Date);
                DateTime level2 = DateTime.Today.AddDays(-settings.Level2Date);
                DateTime level3 = DateTime.Today.AddDays(-settings.Level3Date);
                DateTime level4 = DateTime.Today.AddDays(-settings.Level4Date);
                DateTime level5 = DateTime.Today.AddDays(-settings.Level5Date);
                DateTime level6 = DateTime.Today.AddDays(-settings.Level6Date);

                questions = dbContext.Questions.Where(q =>
                    (q.UserId == userId) && ((q.Level == 1 && q.Date == level1.Date) ||
                                             (q.Level == 2 && q.Date == level2.Date) ||
                                             (q.Level == 3 && q.Date == level3.Date) ||
                                             (q.Level == 4 && q.Date == level4.Date) ||
                                             (q.Level == 5 && q.Date == level5.Date) ||
                                             (q.Level == 6 && q.Date == level6.Date))).ToList();
                if (questions.Count == 0)
                {
                    questions = dbContext.Questions.Where(q => q.UserId == userId && q.Level == 0).ToList();
                }
            }
            return questions;
        }

        public void FillGrid(DataGrid grid,int UserID)
        {
            List<Question> questions = dbContext.Questions.Where(q => q.UserId == UserID && q.Level == 6).ToList();
            grid.ItemsSource = questions;
        }

        public List<string> RandomAnswers()
        {
            return dbContext.Questions.Select(q => q.TurkishWord).ToList();
        }

        public void Update(int id, string englishWord, string turkishWord, int level, DateTime today)
        {
            Question question = dbContext.Questions.FirstOrDefault(q => q.Id == id);
            question.EnglishWord = englishWord;
            question.TurkishWord = turkishWord;
            question.Level = level;
            question.Date = today;
        }
    }
}

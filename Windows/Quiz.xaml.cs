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
using MaterialDesignColors;
using WordMemorize.DatabaseModels;
using WordMemorize.Helpers;

namespace WordMemorize.Windows
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Window
    {
        private static Service service;
        private static List<Question> questions;
        private int totalQuestionCount;
        int nowQuestionNumber = 0;
        private static List<String> rndAnswers;
        private int totalAnswerCount;
        public Quiz(int userId,bool isPractice)
        {
            InitializeComponent();
            service = new Service();
            questions = service.GetQuestions(userId,isPractice);
            totalQuestionCount = questions.Count;
            rndAnswers = service.RandomAnswers();
            totalAnswerCount = rndAnswers.Count;
        }

        private void TakeQuestion()
        {
            question.Text = questions[nowQuestionNumber].EnglishWord;
            Random rnd = new Random();
            int rndNumber = rnd.Next(1, 5);
            switch (rndNumber)
            {
                case 1:
                    PrepareQuestion(A, B, C, D, E);
                    break;
                case 2:
                    PrepareQuestion(B, A, C, D, E);
                    break;
                case 3:
                    PrepareQuestion(C, B, A, D, E);
                    break;
                case 4:
                    PrepareQuestion(D, B, A, C, E);
                    break;
                case 5:
                    PrepareQuestion(E, B, A, C, D);
                    break;
            }
        }

        private void PrepareQuestion(Button a, Button b, Button c, Button d, Button e)
        {
            int[] rndAnsNo = IsThatOkay();

            a.Content = questions[nowQuestionNumber].TurkishWord;
            b.Content = rndAnswers[rndAnsNo[0]];
            c.Content = rndAnswers[rndAnsNo[1]];
            d.Content = rndAnswers[rndAnsNo[2]];
            e.Content = rndAnswers[rndAnsNo[3]];
        }
        private int[] IsThatOkay()
        {
            int[] rndNumbers = new int[4];
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                int rndTempNo = rnd.Next(0, totalAnswerCount);
                for (int z = 0; z < rndNumbers.Length; z++)
                {
                    if ((questions[nowQuestionNumber].TurkishWord == rndAnswers[rndTempNo]) || (rndNumbers[z] == rndTempNo))
                    {
                        do
                        {
                            rndTempNo = rnd.Next(0, totalAnswerCount);
                        }
                        while ((questions[nowQuestionNumber].TurkishWord == rndAnswers[rndTempNo]) || (rndNumbers[z] == rndTempNo));
                    }
                }
                rndNumbers[i] = rndTempNo;
            }
            return rndNumbers;
        }

        private void question_Loaded(object sender, RoutedEventArgs e)
        {
            TakeQuestion();
        }

        private void Answer_OnClick(object sender, RoutedEventArgs e)
        {
            Button btnAnswer = (Button)sender;

            A.IsEnabled = false;
            B.IsEnabled = false;
            C.IsEnabled = false;
            D.IsEnabled = false;
            E.IsEnabled = false;

            if ((string)btnAnswer.Content == questions[nowQuestionNumber].TurkishWord)
            {
                btnAnswer.Background = Brushes.Green;
                Skip.Visibility = Visibility.Collapsed;
                Correct.Visibility = Visibility.Visible;
                questions[nowQuestionNumber].Level++;

                service.Update((int) questions[nowQuestionNumber].Id, questions[nowQuestionNumber].EnglishWord,
                    questions[nowQuestionNumber].TurkishWord, questions[nowQuestionNumber].Level, DateTime.Today);

            }
            else
            {
                btnAnswer.Background = Brushes.Red;
                int level = 0;
                service.Update((int) questions[nowQuestionNumber].Id, questions[nowQuestionNumber].EnglishWord,
                    questions[nowQuestionNumber].TurkishWord, level, DateTime.Today);
            }
        }

        private void Correct_OnClick(object sender, RoutedEventArgs e)
        {
            Correct.Visibility = Visibility.Collapsed;
            Skip.Visibility = Visibility.Visible;
            BtnCleaner();

            nowQuestionNumber++;
            if (nowQuestionNumber < totalQuestionCount)
            {
                TakeQuestion();
            }
            else
            {
                Close();
            }
        }
        private void BtnCleaner()
        {
            A.Background = Brushes.DarkSlateBlue;
            B.Background = Brushes.DarkSlateBlue;
            C.Background = Brushes.DarkSlateBlue;
            D.Background = Brushes.DarkSlateBlue;
            E.Background = Brushes.DarkSlateBlue;

            A.IsEnabled = true;
            B.IsEnabled = true;
            C.IsEnabled = true;
            D.IsEnabled = true;
            E.IsEnabled = true;
        }

        private void Skip_OnClick(object sender, RoutedEventArgs e)
        {
            BtnCleaner();
            nowQuestionNumber++;
            if (nowQuestionNumber < totalQuestionCount)
            {
                TakeQuestion();
            }
            else
            {
                Close();
            }
        }

        private void Closee(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Maximize(object sender, MouseButtonEventArgs e)
        {
            service.MaximizePage(this);
        }

        private void Minimize(object sender, MouseButtonEventArgs e)
        {
            service.MinimizePage(this);
        }
    }
}

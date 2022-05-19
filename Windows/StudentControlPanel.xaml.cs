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
using WordMemorize.DatabaseModels;
using WordMemorize.Helpers;

namespace WordMemorize.Windows
{
    /// <summary>
    /// Interaction logic for StudentControlPanel.xaml
    /// </summary>
    public partial class StudentControlPanel : Window
    {
        private Service service;
        private int userId;
        private Setting setting;
        private wordmemorizeContext dbContext;
        public StudentControlPanel(int userId)
        {
            InitializeComponent();
            service = new Service();
            this.userId = userId;
            setting = service.GetSettings(userId);
        }

        private void learnedWords_Loaded(object sender, RoutedEventArgs e)
        {
            service.FillGrid(learnedWords, userId);
        }

        private void StartQuiz_OnClick(object sender, RoutedEventArgs e)
        {
            Quiz quiz = new Quiz(userId,false);
            quiz.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrepareSettings();
        }

        private void PrepareSettings()
        {
            txtlvl1.Text = setting.Level1Date.ToString();
            txtlvl2.Text = setting.Level2Date.ToString();
            txtlvl3.Text = setting.Level3Date.ToString();
            txtlvl4.Text = setting.Level4Date.ToString();
            txtlvl5.Text = setting.Level5Date.ToString();
            txtlvl6.Text = setting.Level6Date.ToString();

            combolvl1.SelectedItem = combolvl1.Items[0];
            combolvl2.SelectedItem = combolvl1.Items[0];
            combolvl3.SelectedItem = combolvl1.Items[0];
            combolvl4.SelectedItem = combolvl1.Items[0];
            combolvl5.SelectedItem = combolvl1.Items[0];
            combolvl6.SelectedItem = combolvl1.Items[0];

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            setting.Level1Date = Convert.ToInt32(txtlvl1.Text);
            setting.Level2Date = Convert.ToInt32(txtlvl2.Text);
            setting.Level3Date = Convert.ToInt32(txtlvl3.Text);
            setting.Level4Date = Convert.ToInt32(txtlvl4.Text);
            setting.Level5Date = Convert.ToInt32(txtlvl5.Text);
            setting.Level6Date = Convert.ToInt32(txtlvl6.Text);

            dbContext.SaveChanges();
        }

        private void Practice_OnClick(object sender, RoutedEventArgs e)
        {
            Quiz quiz = new Quiz(userId, true);
            quiz.Show();
        }
    }
}

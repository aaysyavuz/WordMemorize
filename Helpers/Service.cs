using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WordMemorize.DatabaseModels;

namespace WordMemorize.Helpers
{
    public class Service
    {
        public static bool IsRegistered(string Email)
        {
            wordmemorizeContext databaseContext = new wordmemorizeContext();
            return databaseContext.Users.FirstOrDefault(user => user.Email == Email) != null;
        }

        public static void CloseApp()
        {
            Application.Current.Shutdown();
        }

        public static void HidePage(Window window)
        {
            window.Hide();
        }

        public static void MinimizePage(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        public static void MaximizePage(Window window)
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
        public static void AddWord(){}
    }
}

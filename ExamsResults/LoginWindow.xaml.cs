using ExamsResults.Classes;
using System;
using System.Windows;

namespace ExamsResults
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "admin@mail.ru" && Pass.Password == "123")
            {
                MessageBox.Show("Добро пожаловать, администратор!");

                MainWindow mainWindow = new MainWindow("admin");
                mainWindow.Show();
                this.Close();
            }
            else if (Email.Text == "user@mail.ru" && Pass.Password == "321")
            {
                MessageBox.Show("Добро пожаловать, пользователь!");

                MainWindow mainWindow = new MainWindow("user");
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
        }
    }
}

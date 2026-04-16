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
using System.Windows.Shapes;

namespace ExamsResults
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        MainWindow _mainWindow = new MainWindow();
        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Email.Text) && !string.IsNullOrWhiteSpace(Pass.Text))
            {
                _mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите почту или пароль", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

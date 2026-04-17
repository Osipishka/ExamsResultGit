using ExamsResults.Classes;
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
            try
            {
               var userObj = Classes.AppConnect.context.Users.FirstOrDefault(x => x.Login == Email.Text
                                                                         && x.Pass == Convert.ToUInt32(Pass.Password));
                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch(userObj.Role)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте администратор!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            _mainWindow.Show();
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте пользователь", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            _mainWindow.Show();
                            break;
                        default:
                            MessageBox.Show("Здравствуйте администратор!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Неверная почта или пароль", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
                
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

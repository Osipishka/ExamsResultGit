using System;
using System.Windows;
using System.Windows.Controls;

namespace ExamsResults
{
    public partial class AddExamWindow : Window
    {
        private MainWindow _mainWindow;

        public AddExamWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SubjectBox.Text) ||
                    string.IsNullOrWhiteSpace(DateBox.Text) ||
                    string.IsNullOrWhiteSpace(ExaminerBox.Text) ||
                    string.IsNullOrWhiteSpace(StudentBox.Text) ||
                    string.IsNullOrWhiteSpace(GroupBox.Text) ||
                    string.IsNullOrWhiteSpace(ScoreBox.Text))
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                int score = int.Parse(ScoreBox.Text);

                var exam = new ExamViewModel(
                    SubjectBox.Text,
                    DateBox.Text,
                    (ExaminerBox.Text, 1500),
                    StudentBox.Text,
                    GroupBox.Text,
                    score,
                    40
                );

                _mainWindow.AddExam(exam);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
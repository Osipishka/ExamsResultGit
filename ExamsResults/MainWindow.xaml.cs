
using ExamsResults.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExamsResults
{
    public partial class MainWindow : Window
    {
        private List<ExamViewModel> _allExams;
        private List<ExamViewModel> _filteredExams;

        private int _currentPage = 1;
        private int _pageSize = 5;
        private string _role;

        private Dictionary<int, string> Groups = new Dictionary<int, string>()
        {
            {1, "ИСП-23"},
            {2, "СА-23"},
            {3, "И-23"},
            {4, "ИСП-24"},
            {5, "СА-24"},
            {6, "И-24"},
            {7, "ИСП-25"},
            {8, "СА-25"},
            {9, "И-25"}
        };

        private Dictionary<int, (string Name, decimal Salary)> Teachers =
            new Dictionary<int, (string, decimal)>()
        {
            {1, ("Прохоров П. П.", 1500)},
            {2, ("Смирнова И. А.", 1700)},
            {3, ("Кузнецов А. В.", 1600)},
            {4, ("Лебедева Н. С.", 1800)},
            {5, ("Соколов Д. М.", 1400)},
            {6, ("Попова Е. В.", 2000)},
            {7, ("Волков Р. А.", 1750)},
            {8, ("Зайцева О. И.", 1650)},
            {9, ("Павлов С. Н.", 1900)},
            {10,("Романова А. Д.", 1550)}
        };

        public MainWindow(string role = "user")
        {
            InitializeComponent();

            _role = role;

            // 👇 ВОТ ЭТО ГЛАВНОЕ
            if (_role != "admin")
            {
                AddExamButton.Visibility = Visibility.Collapsed;
            }

            LoadData();

            foreach (var g in Groups.Values)
                GroupFilter.Items.Add(g);

            _filteredExams = _allExams;

            LoadPage();
        }

        private void LoadData()
        {
            _allExams = new List<ExamViewModel>
            {
                new ExamViewModel("Математика","28.05.2026", Teachers[1], "Иванов И.И.", Groups[1],38,40),
                new ExamViewModel("Русский","30.05.2026", Teachers[2], "Петров А.С.", Groups[1],65,40),
                new ExamViewModel("Информатика","01.06.2026", Teachers[1], "Сидоров А.Д.", Groups[2],44,46),
                new ExamViewModel("Физика","03.06.2026", Teachers[5], "Козлов Е.П.", Groups[3],58,41),
                new ExamViewModel("Английский","05.06.2026", Teachers[4], "Морозов Д.А.", Groups[4],39,40),
                new ExamViewModel("История","07.06.2026", Teachers[7], "Васильева О.Н.", Groups[5],72,40),
                new ExamViewModel("Обществознание","09.06.2026", Teachers[6], "Новиков П.И.", Groups[6],43,45),
                new ExamViewModel("Биология","11.06.2026", Teachers[9], "Фёдорова М.А.", Groups[7],55,40),
                new ExamViewModel("Химия","13.06.2026", Teachers[10], "Соколов А.В.", Groups[8],81,40),
                new ExamViewModel("Литература","15.06.2026", Teachers[3], "Михайлова Т.Т.", Groups[9],37,40),
            };
        }

        private void ApplyFilters()
        {
            if (_allExams == null)
                return;

            string search = SearchBox.Text?.ToLower() ?? "";

            string selectedGroup = (GroupFilter.SelectedItem as ComboBoxItem)?.Content?.ToString()
                                   ?? GroupFilter.SelectedItem?.ToString();

            _filteredExams = _allExams.Where(x =>
                (string.IsNullOrEmpty(search) || x.NameSubject.ToLower().Contains(search)) &&
                (selectedGroup == "Все группы" || x.GroupStudent == selectedGroup)
            ).ToList();

            _currentPage = 1;
            LoadPage();
        }

        private void LoadPage()
        {
            var source = _filteredExams ?? _allExams;

            var pageData = source
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            dataGridExams.ItemsSource = pageData;
            PageText.Text = _currentPage.ToString();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void GroupFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage * _pageSize < _filteredExams.Count)
            {
                _currentPage++;
                LoadPage();
            }
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadPage();
            }
        }

        private void AddExam_Click(object sender, RoutedEventArgs e)
        {
            AddExamWindow window = new AddExamWindow(this);
            window.ShowDialog();
        }
            
        public void AddExam(ExamViewModel exam)
        {
            _allExams.Add(exam);
            ApplyFilters(); // автоматически обновит таблицу
        }
    }


    public class ExamViewModel
    {
        public string NameSubject { get; set; }
        public string DateExam { get; set; }
        public string NameExaminer { get; set; }
        public string NameStudent { get; set; }
        public string GroupStudent { get; set; }

        public int ExamsResult { get; set; }
        public int MinScore { get; set; }

        public decimal Salary { get; set; }

        public bool IsPassed => ExamsResult >= MinScore;
        public string Result => IsPassed ? "Сдал" : "Не сдал";

        public ExamViewModel(string subject, string date,
            (string Name, decimal Salary) teacher,
            string student, string group,
            int result, int minScore)
        {
            NameSubject = subject;
            DateExam = date;
            NameExaminer = teacher.Name;
            Salary = teacher.Salary;
            NameStudent = student;
            GroupStudent = group;
            ExamsResult = result;
            MinScore = minScore;
        }
    }
}
// Classes/ExamDisplayModel.cs
using System;

namespace ExamsResults.Classes
{
    public class ExamResultViewModel
    {
        public string NameSubject { get; set; }
        public string DateExam { get; set; }
        public string NameExaminer { get; set; }
        public string NameStudent { get; set; }
        public int? GroupStudent { get; set; }
        public string Result { get; set; }
        public int OriginalId { get; set; }
    }
}
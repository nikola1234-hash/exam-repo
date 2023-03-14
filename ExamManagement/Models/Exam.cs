using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class Exam
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string LecturerName { get; set; }
        public DateTime StartingHour { get; set; }
        public int TotalTime { get; set; }
        public bool RandomSorting { get; set; }
        public List<Question> Questions { get; set; }
    }
}

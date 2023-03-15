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
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public string LecturerName { get; set; }
        public TimeSpan TotalTime { get;set;}
        public bool RandomSorting { get; set; }
        public List<Question> Questions { get; set; }




        public Exam()
        {
            Name = string.Empty;
            Id = Guid.NewGuid();
            StartDateTime = DateTime.Now;
            LecturerName = string.Empty;
            RandomSorting = false;
            TotalTime = new TimeSpan(0, 0, 0);
            Questions = new List<Question>();

        }

    }
}

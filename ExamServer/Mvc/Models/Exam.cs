using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamServer.Mvc.Models
{
    class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string LecturerName { get; set; }
        public TimeSpan StartingHour { get; set; }
        public int TotalTime { get; set; }
        public bool RandomizeQuestions { get; set; }
        public List<Question> Questions { get; set; }

        // Example function to shuffle the questions list if randomizeQuestions is true
        public void ShuffleQuestions()
        {
            if (RandomizeQuestions)
            {
                var random = new Random();
                Questions = Questions.OrderBy(x => random.Next()).ToList();
            }
        }

        // Example function to get the total number of questions in the exam
        public int GetTotalNumberOfQuestions()
        {
            return Questions.Count;
        }

        // Example function to get the duration of the exam
        public TimeSpan GetDuration()
        {
            return new TimeSpan(0, TotalTime, 0);
        }

        // Example function to get the end time of the exam based on the starting hour and duration
        public DateTime GetEndTime()
        {
            return Date.Date + StartingHour + GetDuration();
        }
    }
}

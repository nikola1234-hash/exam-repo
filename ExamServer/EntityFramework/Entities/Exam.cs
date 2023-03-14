using ExamServer.Mvc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamServer.EntityFramework.Entities
{
    public class Exam : BaseObject
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string LecturerName { get; set; }
        public long TickCount { get; set; }

      

        [NotMapped]
        public TimeSpan StartingHour
        {
            get
            {
                return new TimeSpan(TickCount);
            }
            set
            {
                TickCount = value.Ticks;
            }
        }
        public int TotalTime { get; set; }
        public bool RandomizeQuestions { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

    }
}


using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamServer.EntityFramework.Entities
{
    public class Exam
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public string LecturerName { get; set; }
        public string StartingHour
        {
            get
            {
                return StartDateTime.Hour.ToString();
            }
        }
        public TimeSpan TotalTime { get; set; }
        public bool RandomSorting { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}

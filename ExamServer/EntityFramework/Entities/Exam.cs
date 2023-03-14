using ExamServer.Mvc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamServer.EntityFramework.Entities
{
    public class Exam : BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string LecturerName { get; set; }
        public string StartingHour { get; set; }
        public int TotalTime { get; set; }
        public bool RandomSorting { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}

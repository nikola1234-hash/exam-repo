using Server.Mvc.Models;

namespace Server.EntityFramework.Entities
{
    public class GradeEntity : BaseObject
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public Exam Exam { get; set; }
        public double Grade { get; set; }
        public List<Error> Errors { get; set; }
        public GradeEntity()
        {

        }
        public GradeEntity(GradingModel model)
        {
            StudentId = model.StudentId;
            StudentName = model.StudentName;
            Exam = model.Exam;
            Grade = model.Grade;
            Errors = model.Errors;
        }
    }
}

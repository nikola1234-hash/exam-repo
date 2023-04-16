namespace EasyTestMaker.Models
{
    public class GradingData
    {
        public GradingData(StudentTest studentExam, int studentId, Test test)
        {
            StudentExam = studentExam;
            StudentId = studentId;
            Test = test;
        }

        public StudentTest StudentExam { get; set; }
        public int StudentId { get; set; }
        public Test Test { get; set; }
    }
}

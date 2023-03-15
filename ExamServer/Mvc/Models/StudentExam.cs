namespace ExamServer.Mvc.Models
{
    public class StudentExam
    {
        public string StudentName { get; set; }
        public List<int> SelectedAnswers { get; set; }
        public StudentExam()
        {
            SelectedAnswers = new List<int>();
        }


    }
}

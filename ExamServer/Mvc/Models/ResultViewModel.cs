namespace ExamServer.Mvc.Models
{

        public class ResultViewModel
        {
            public int UserId { get; set; }
            public List<Result> Results { get; set; }

            public ResultViewModel()
            {

            }

            public ResultViewModel(int userId, List<Result> results)
            {
                UserId = userId;
                Results = results;
            }
        
        }
}

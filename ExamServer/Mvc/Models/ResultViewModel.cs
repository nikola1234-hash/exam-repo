namespace Server.Mvc.Models
{

        public class ResultViewModel
        {
            public string User { get; set; }
            public List<Result> Results { get; set; }

            public ResultViewModel()
            {

            }

            public ResultViewModel(string user, List<Result> results)
            {
                User = user;
                Results = results;
            }
        
        }
}

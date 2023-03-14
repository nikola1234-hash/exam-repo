using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class ResultViewModel
    {
        public string User { get; set; }
        public List<Result> Results {get;set;}

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

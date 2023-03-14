using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class ResultViewModel
    {
        public int UserId { get; set; }
        public List<Result> Results {get;set;}

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

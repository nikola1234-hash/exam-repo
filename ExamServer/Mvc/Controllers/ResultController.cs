using ExamServer.EntityFramework;
using ExamServer.EntityFramework.Entities;
using ExamServer.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ExamDbContext _context;

        public ResultController(ExamDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var result = _context.Results.ToList();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult AddResult(ResultViewModel result)
        {
            ExamResult examResults = new ExamResult();
            examResults.User = result.UserId.ToString();
            examResults.Results = new List<QuestionResult>();
            foreach(var r in result.Results) 
            {
                r.Question.ImageUrl = string.Empty;
                QuestionResult qr = new QuestionResult();
                qr.Questions = new ExamServer.EntityFramework.Entities.Question();
                qr.Questions.Text = r.Question.Text;
                qr.Questions.ImageUrl = string.Empty;
                Answer answer = new Answer();
                
                answer.Text = r.Answer.Text;
                answer.IsCorrect = r.Answer.IsCorrect;
                qr.Questions.Answers = new List<Answer>();
                qr.Questions.Answers.Add(answer);
                examResults.Results.Add(qr);
            }
            _context.Results.Add(examResults);
            var i = _context.SaveChanges();
            return i > 0 ? Ok() : BadRequest();
        }
    }
}

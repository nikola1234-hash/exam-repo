using Server.EntityFramework.Entities;
using Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Mvc.Controllers
{
    [Route("api/exams/{examId}/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IGenericRepository<Question> _context;
        private readonly IGenericRepository<Exam> _examContext;

        public QuestionController(IGenericRepository<Question> context, IGenericRepository<Exam> examContext)
        {
            _context = context;
            _examContext = examContext;
        }

  
        // GET api/exams/{examId}/questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions(int examId)
        {



            var exam = await _examContext.GetById(examId);

            if (exam.Questions == null)
            {
                return NotFound();
            }

            return exam.Questions.ToList();
        }

        // GET api/exams/{examId}/questions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int examId, int id)
        {
            var exam = await _examContext.GetById(examId);
            if (exam == null)
            {
                return NotFound();
            }

            var question = exam.Questions.FirstOrDefault(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // POST api/exams/{examId}/questions
        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion(int examId, Question question)
        {
            var exam = await _examContext.GetById(examId);

            if (exam == null)
            {
                return NotFound();
            }

            exam.Questions.Add(question);
            return CreatedAtAction(nameof(GetQuestion), new { examId, id = question.Id }, question);
        }

        // PUT api/exams/{examId}/questions/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateQuestion(int examId, int id, Question question)
        //{
        //    if (id != question.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var exam = await _examContext.GetById(examId);

        //    if (exam == null)
        //    {
        //        return NotFound();
        //    }
        //    var questionToUpdate = exam.Questions.Where(q => q.Id == id).FirstOrDefault();
        //    var q = 
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // DELETE api/exams/{examId}/questions/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteQuestion(int examId, int id)
        //{
        //var exam = await _context.Exams.FindAsync(examId);

        //if (exam == null)
        //{
        //    return NotFound();
        //}

        //var question = exam.Questions.FirstOrDefault(q => q.Id == id);

        //if (question == null)
        //{
        //    return NotFound();
        //}

        //exam.Questions.Remove(question);
        //await _context.SaveChangesAsync();

        //return NoContent();
    }
}


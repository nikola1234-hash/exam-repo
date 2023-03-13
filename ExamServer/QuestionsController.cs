using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer
{
    [Route("api/exams/{examId}/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ExamManagementContext _context;

        public QuestionsController(ExamManagementContext context)
        {
            _context = context;
        }

        // GET api/exams/{examId}/questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions(int examId)
        {
            var exam = await _context.Exams.FindAsync(examId);

            if (exam == null)
            {
                return NotFound();
            }

            var questions = exam.Questions.ToList();

            if (exam.RandomizeQuestions)
            {
                questions = questions.OrderBy(q => Guid.NewGuid()).ToList();
            }

            return questions;
        }

        // GET api/exams/{examId}/questions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int examId, int id)
        {
            var exam = await _context.Exams.FindAsync(examId);

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
            var exam = await _context.Exams.FindAsync(examId);

            if (exam == null)
            {
                return NotFound();
            }

            exam.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestion), new { examId = examId, id = question.Id }, question);
        }

        // PUT api/exams/{examId}/questions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int examId, int id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            var exam = await _context.Exams.FindAsync(examId);

            if (exam == null)
            {
                return NotFound();
            }

            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/exams/{examId}/questions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int examId, int id)
        {
            var exam = await _context.Exams.FindAsync(examId);

            if (exam == null)
            {
                return NotFound();
            }

            var question = exam.Questions.FirstOrDefault(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            exam.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
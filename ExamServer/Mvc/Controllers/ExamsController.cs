using ExamServer.EntityFramework.Entities;
using ExamServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ICrudService<Exam> _context;

        public ExamsController(ICrudService<Exam> context)
        {
            _context = context;
        }

        // GET api/exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
           var result =  await _context.GetAll();
           return result.ToList();
        }

        // GET api/exams/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            var exam = await _context.GetById(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        // POST api/exams
        [HttpPost]
        public async Task<ActionResult<Exam>> CreateExam(Exam exam)
        {
            await _context.Add(exam);

            return CreatedAtAction(nameof(GetExam), new { id = exam.Id }, exam);
        }

        // PUT api/exams/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, Exam exam)
        {
            if (id != exam.Id)
            {
                return BadRequest();
            }
            await _context.Update(id, exam);

            return NoContent();
        }

        // DELETE api/exams/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            int output = await _context.Remove(id);
            if(output > 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }

}

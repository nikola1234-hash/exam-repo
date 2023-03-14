using ExamServer.EntityFramework;
using ExamServer.EntityFramework.Entities;
using ExamServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamServer.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ICrudService<Exam> _context;
        private readonly ExamDbContext _dbContext;
        public ExamController(ICrudService<Exam> context, ExamDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        // GET api/exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
       
           var exams = _dbContext.Exams.Include(s => s.Questions).ThenInclude(s => s.Answers).ToList();
       
          
           return exams;
        }

        // GET api/exams/{id}
        [HttpGet("{name}")]
        public async Task<ActionResult<Exam>> GetExam(string name)
        {
            var exam = _dbContext.Exams.Include(s => s.Questions).ThenInclude(s => s.Answers).FirstOrDefault(s => s.Name == name);

            if (exam == null)
            {
                return NotFound();
            }
           
            return exam;
        }

        // POST api/exam
        [HttpPost]
        [ProducesResponseType(201)]

        public async Task<ActionResult<Exam>> CreateExam([FromBody]Exam exam)
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

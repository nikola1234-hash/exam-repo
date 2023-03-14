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

        public async Task<ActionResult<List<Exam>>> CreateExams([FromBody]List<Exam> exams)
        {
            await _dbContext.Exams.AddRangeAsync(exams);
            await _dbContext.SaveChangesAsync();

            return Ok();
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

    }

}

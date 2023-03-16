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

        private readonly ExamDbContext _dbContext;
        public ExamController(ExamDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        // GET api/exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
          
           var exams = _dbContext.Exams.Include(s => s.Questions).ThenInclude(s=> s.Answers).ToList();
       
          
           return exams;
        }

        // GET api/exams/{id}
        [HttpGet("{name}")]
        public async Task<ActionResult<Exam>> GetExam(string name)
        {
            var exam = _dbContext.Exams.Include(s => s.Questions).ThenInclude(s => s.Answers).FirstOrDefault(s => s.Name.ToLower() == name.ToLower());

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
            var dbExams = _dbContext.Exams.ToList();
            foreach (var exam in exams)
            {
                if (dbExams.FirstOrDefault(s=> s.Id == exam.Id) != null)
                {
                    continue;
                }
                else
                {
                    await _dbContext.AddAsync(exam);
                }
            }
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/exams/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(Guid id, Exam exam)
        {
            var fromDb = await _dbContext.Exams.FindAsync(id);
            if (fromDb != null)
            {
                _dbContext.Entry(fromDb).CurrentValues.SetValues(exam);
                _dbContext.Entry(fromDb).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            return Ok();
        }

    }

}

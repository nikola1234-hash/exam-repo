using Server.EntityFramework;
using Server.EntityFramework.Entities;
using Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Runtime.InteropServices;

namespace Server.Mvc.Controllers
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

        // Gets all exams and related questions and answers
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
     

        // Adds a list of exams from json to db
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

        // Updates edited exam
        // And its questions and answers
        [HttpPut]
        public async Task<IActionResult> UpdateExam(Exam exam)
        {
            var fromDb = await _dbContext.Exams.Where(s => s.Id == exam.Id).Include(s=> s.Questions).ThenInclude(s=> s.Answers).FirstOrDefaultAsync();
            if (fromDb != null)
            {

                fromDb.Questions.Clear();
                foreach(var question in exam.Questions)
                {
                    fromDb.Questions.Add(question);
                    fromDb.LecturerName = exam.LecturerName;
                    fromDb.Name = exam.Name;
                    fromDb.StartDateTime = exam.StartDateTime;
                    fromDb.TotalTime = exam.TotalTime;
                    fromDb.RandomSorting = exam.RandomSorting;
                  
                }
                
                _dbContext.Update(fromDb);
              
                await _dbContext.SaveChangesAsync();
            }
            return Ok();
        }

    }

}

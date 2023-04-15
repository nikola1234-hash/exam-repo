using ExamServer.EntityFramework;
using ExamServer.EntityFramework.Entities;
using ExamServer.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamServer.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public GradeController(ServerDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var result = _context.Grades.Include(s => s.Errors).Include(s=> s.Exam).ThenInclude(s=> s.Questions);
            return Ok(result);
        }
        [HttpPost]
        public ActionResult AddResult(GradingModel result)
        {

            result.CalculateGrade();
            var grade = new GradeEntity(result);
            _context.Grades.Add(grade);
            var i = _context.SaveChanges();
            return i > 0 ? Ok() : BadRequest();
        }
    }
}

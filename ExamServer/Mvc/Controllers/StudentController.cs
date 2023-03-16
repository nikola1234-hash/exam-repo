using ExamServer.EntityFramework;
using ExamServer.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ExamDbContext _dbContext;

        public StudentController(ExamDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var entities = _dbContext.Students.ToList();
            return Ok(entities);

        }

        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            var entity = await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return Ok(entity.Entity);

        }
    }
}

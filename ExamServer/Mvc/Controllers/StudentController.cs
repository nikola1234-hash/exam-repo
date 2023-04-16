using Server.EntityFramework;
using Server.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Server.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly TestDbContext _dbContext;

        public StudentController(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var entities = _dbContext.Students.ToList();
            return Ok(entities);

        }
        /// <summary>
        /// Creates new student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            var entity = await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return Ok(entity.Entity);

        }
    }
}

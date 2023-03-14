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
    public class ResultController : ControllerBase
    {
        private readonly ExamDbContext _context;

        public ResultController(ExamDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var result = _context.ExamResults.Include(s => s.Errors);
            return Ok(result);
        }
        [HttpPost]
        public ActionResult AddResult(ExamResult result)
        {

           
            _context.ExamResults.Add(result);
            var i = _context.SaveChanges();
            return i > 0 ? Ok() : BadRequest();
        }
    }
}

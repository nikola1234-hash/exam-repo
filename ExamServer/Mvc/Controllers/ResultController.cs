using ExamServer.EntityFramework;
using ExamServer.EntityFramework.Entities;
using ExamServer.Mvc.Models;
using ExamServer.Services;
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

        private readonly GradingService gradingService;


        public ResultController(ExamDbContext context)
        {
            _context = context;
            gradingService = new GradingService();
            
        }


        //Get exam results and errors
        [HttpGet]
        public ActionResult Get()
        {
            var result = _context.ExamResults.Include(s => s.Errors);
            return Ok(result);
        }

        //Gets exam result by ID
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _context.ExamResults.Where(s => s.Student.Id == id).Include(s => s.Errors).Include(s=> s.Exam);
            return Ok(result);
        }

        /// <summary>
        /// Recieves exam result and grades student and stores it to db
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddResult(GradingData data)
        {

            var result = gradingService.Grade(data);

            _context.ExamResults.Add(result);
            var i = _context.SaveChanges();
            return i > 0 ? Ok() : BadRequest();
        }
    }
}

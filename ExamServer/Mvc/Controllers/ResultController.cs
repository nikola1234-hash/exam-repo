﻿using ExamServer.EntityFramework;
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
        [HttpGet]
        public ActionResult Get()
        {
            var result = _context.ExamResults.Include(s => s.Errors);
            return Ok(result);
        }

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

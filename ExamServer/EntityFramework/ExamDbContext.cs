﻿using Server.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static System.Net.Mime.MediaTypeNames;

namespace Server.EntityFramework
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {
        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().HasMany(i => i.Questions);
            modelBuilder.Entity<Question>().HasMany(i => i.Answers);

            modelBuilder.Entity<Exam>()
            .Property(s => s.TotalTime)
            .HasConversion(new TimeSpanToTicksConverter());
            modelBuilder.Entity<User>().HasData(
           new User
           {
               Id = 1,
               Username = "Lector",
               Password = "123",
           });
            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = 2,
                  Username = "Student",
                  Password = "123",
              });
        }
     
    }
}

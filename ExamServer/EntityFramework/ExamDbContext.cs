using ExamServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExamServer.EntityFramework
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {
        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<GradeEntity> Grades { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().HasMany(i => i.Questions);
            modelBuilder.Entity<Question>().HasMany(i => i.Answers);
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

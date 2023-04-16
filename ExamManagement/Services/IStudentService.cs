using EasyTestMaker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTestMaker.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync();
        Task SaveExamResult(int studentId, string studentName, Guid id, int grade, List<Error> errors);
        Task<Exam> StartExam(Exam exam);
        Task<bool> SubmitExamAnswers(Exam exam, int studentId, StudentExam studentExam);
        Task<Student> UpdateStudentInformation(Student student);
    }
}
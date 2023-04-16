using EasyTestMaker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTestMaker.Services
{
    public interface IExamService
    {
        void AddExam(Exam exam);
        Task<Exam> GetExamByName(string name);
        Task<List<ExamResult>> GetExamResults();
        Task<List<ExamResult>> GetExamResultsByName(int id);
        Task<List<Exam>> GetExamsAsync();
        Task<Exam> GetExamsStatistics(Guid examId);
        Task PutToServer(Exam exam);
        Task SubmitExamResult(ExamResult result);
        Task UpdateExam(Exam exam, bool pushToServer);
        Task UpdateExamJson(Exam exam);
    }
}
using EasyTestMaker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTestMaker.Services
{
    public interface ITestService
    {
        void AddExam(Test exam);
        Task<Test> GetExamByName(string name);
        Task<List<TestResult>> GetExamResults();
        Task<List<TestResult>> GetExamResultsByName(int id);
        Task<List<Test>> GetExamsAsync();
        Task<Test> GetExamsStatistics(Guid examId);
        Task PutToServer(Test exam);
        Task SubmitExamResult(TestResult result);
        Task UpdateExam(Test exam, bool pushToServer);
        Task UpdateExamJson(Test exam);
    }
}
using EasyTestMaker.Models;
using EasyTestMaker.Repositories;

namespace EasyTestMaker.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBaseRepository<Exam> ExamRepository { get; set; }
        IBaseRepository<GradeEntity> GradeRepository { get; set; }
    }
}
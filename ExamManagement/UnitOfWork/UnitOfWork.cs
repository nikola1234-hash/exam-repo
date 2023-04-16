using EasyTestMaker.Models;
using EasyTestMaker.Repositories;

namespace EasyTestMaker.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBaseRepository<Exam> ExamRepository { get; set; }
        public IBaseRepository<GradeEntity> GradeRepository { get; set; }

        public UnitOfWork()
        {
            ExamRepository = new BaseRepository<Exam>();
            GradeRepository = new BaseRepository<GradeEntity>();

        }
    }
}

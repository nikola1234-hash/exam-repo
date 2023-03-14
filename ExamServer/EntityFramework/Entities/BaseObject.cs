using System.ComponentModel.DataAnnotations;

namespace ExamServer.EntityFramework.Entities
{
    public class BaseObject
    {
        [Key]
        public int Id { get; set; }
    }
}

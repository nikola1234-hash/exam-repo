using Azure.Identity;

namespace ExamServer.EntityFramework.Entities
{
    public class User : BaseObject
    { 
        public string Username { get; set; }
        public string Password { get; set; }

    }
}

namespace Server.EntityFramework.Entities
{
    public class Result : BaseObject
    {
        Question Question { get; set; }
        Answer Answer { get; set; }
    }
}

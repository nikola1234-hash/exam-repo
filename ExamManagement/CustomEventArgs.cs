namespace ExamManagement
{
    public class CustomEventArgs
    {
        public object Args;
        public int Index { get; set; }
        public CustomEventArgs(object args, int index)
        {
            Args = args;
            Index = index;
        }
    }
}
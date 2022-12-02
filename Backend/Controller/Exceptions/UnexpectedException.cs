namespace Controller.Exceptions
{
    public class UnexpectedException
    {
        public object Result { get; init; }

        public UnexpectedException(Exception e)
        {
            Result = new
            {
                Exception = e.GetType().ToString(),
                Traceback = e.StackTrace
            };
        }
    }
}

namespace Domain.Classes
{
    public enum Type
    {
        Success,
        Error,
    }

    public class ReturnType<T>
    {
        public Type type { get; set; }
        public T? message { get; set; }
    }
}


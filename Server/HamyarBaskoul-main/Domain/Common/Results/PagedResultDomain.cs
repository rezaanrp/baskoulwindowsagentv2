

namespace Domain.Classes
{
    public class PagedResultDomain<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}


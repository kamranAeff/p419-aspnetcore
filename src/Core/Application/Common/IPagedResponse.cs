namespace Application.Common
{
    public interface IPagedResponse<T>
            where T : class
    {
        public int Page { get; }
        public int Size { get; }
        public int Count { get; }
        public int Pages { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
        public IEnumerable<T> Items { get; }
    }
}

namespace WebUI.Models.Common
{
    public class PagedResponse<T>
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public int Count { get; set; }

        public int Pages { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}

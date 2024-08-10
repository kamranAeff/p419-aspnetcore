using Application.Extensions;

namespace Application.Common
{
    class PagedResponse<T> : IPagedResponse<T>
            where T : class
    {
        public PagedResponse(IPageable pageable, IEnumerable<T> items, int totalCount)
        {
            this.Page = pageable.Page;
            this.Size = pageable.Size;

            this.Count = totalCount;

            this.Pages = (int)Math.Ceiling(totalCount * 1D / pageable.Size);
            this.Items = items;
        }
        public int Page { get; }
        public int Size { get; }

        public int Count { get; }

        public int Pages { get; }

        public bool HasPrevious => this.Page > 1;

        public bool HasNext => this.Page < this.Pages;

        public IEnumerable<T> Items { get; }
    }
}

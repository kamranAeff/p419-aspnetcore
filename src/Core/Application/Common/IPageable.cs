namespace Application.Common
{
    public interface IPageable
    {
        public int Page { get; }
        public int Size { get; }
    }
}

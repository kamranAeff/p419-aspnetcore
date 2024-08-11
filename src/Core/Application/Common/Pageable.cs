using Microsoft.AspNetCore.Mvc;

namespace Application.Common
{
    public class Pageable : IPageable
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}

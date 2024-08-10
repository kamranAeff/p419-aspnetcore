using Domain.StableModels;

namespace Application.Common
{
    public interface ISortable
    {
        public string Column { get;}
        public SortOrders Order { get;}
    }

}

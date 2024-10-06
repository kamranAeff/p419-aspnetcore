using Application.Common;
using Application.Extensions;
using Application.Services;
using Domain.StableModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Queries.BasketGetAllQuery
{
    public class BasketGetAllRequest : IRequest<IEnumerable<BasketItem>>, ISortable
    {
        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}

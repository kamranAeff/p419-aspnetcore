﻿using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.CategoriesModule.Queries.CategoriesGetAllQuery
{
    public class CategoriesGetAllRequest : IRequest<IEnumerable<Category>>, ISortable
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
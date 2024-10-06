﻿using Domain.Entities;
using MediatR;

namespace Application.Modules.ProductsModule.Commands.BasketInteractCommand
{
    public class BasketInteractRequest : IRequest<Basket>
    {
        public Guid ProductCardId { get; set; }
        public int Count { get; set; }
    }
}

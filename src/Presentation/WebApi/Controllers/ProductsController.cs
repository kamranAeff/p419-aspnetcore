﻿using Application.Modules.ProductsModule.Commands.BasketInteractCommand;
using Application.Modules.ProductsModule.Commands.ProductAddCommand;
using Application.Modules.ProductsModule.Commands.ProductEditCommand;
using Application.Modules.ProductsModule.Commands.ProductRemoveCommand;
using Application.Modules.ProductsModule.Queries.BasketGetAllQuery;
using Application.Modules.ProductsModule.Queries.ProductGetByIdQuery;
using Application.Modules.ProductsModule.Queries.ProductGetBySlugQuery;
using Application.Modules.ProductsModule.Queries.ProductPagesQuery;
using Application.Modules.ProductsModule.Queries.ProductsGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Middlewares;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromRoute] ProductsGetAllRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [AllowAnonymous]
        [HttpGet("{page:int:min(1)}/{size:int:min(2)}")]
        public async Task<IActionResult> GetAll([FromQuery] ProductPagedRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] ProductGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [AllowAnonymous]
        [HttpGet("{slug:minlength(3):slug}")]
        public async Task<IActionResult> GetBySlug([FromRoute] ProductGetBySlugRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [HttpPost]
        [Transaction]
        public async Task<IActionResult> Add([FromForm] ProductAddRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, dto);
        }

        [Transaction]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductEditRequest request)
        {
            request.Id = id;
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [Transaction]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] ProductRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpPost("basket-interact")]
        public async Task<IActionResult> BasketInteract([FromBody] BasketInteractRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [HttpGet("basket")]
        public async Task<IActionResult> Basket([FromRoute] BasketGetAllRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }
    }
}

using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Models.Common;

namespace WebApi
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            ApiResponse response = null;
            switch (context.Exception)
            {
                case NotFoundException nfEx:
                    response = ApiResponse.Fail(StatusCodes.Status404NotFound, nfEx.Message);
                    break;
                case BadRequestException brEx:
                    response = ApiResponse.Fail(StatusCodes.Status400BadRequest, brEx.Errors, brEx.Message);
                    break;
                default:
                    response = ApiResponse.Fail(StatusCodes.Status500InternalServerError, "ServerError");
                    break;
            }

            context.Result = new JsonResult(response)
            {
                StatusCode = response.Code
            };
        }
    }
}

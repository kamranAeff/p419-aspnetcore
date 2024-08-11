using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Behaviors
{
    public class PageableBinderPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor ctx;

        public PageableBinderPipelineBehavior(IHttpContextAccessor ctx)
        {
            this.ctx = ctx;
        }


        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is Pageable pagedResponse)
            {
                pagedResponse.Page = Convert.ToInt32(ctx.HttpContext.GetRouteValue("page"));
                pagedResponse.Size = Convert.ToInt32(ctx.HttpContext.GetRouteValue("size"));
            }

            return next();
        }
    }
}

using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Middlewares
{
    class DbTransactionMiddleware
    {
        private readonly RequestDelegate next;

        public DbTransactionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, DbContext db)
        {
            if (context.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
            {
                await next(context);
                return;
            }

            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;

            var attribute = endpoint?.Metadata.GetMetadata<TransactionAttribute>();

            if (attribute == null)
            {
                await next(context);
                return;
            }

            IDbContextTransaction transaction = null;


            try
            {
                transaction = db.Database.BeginTransaction();
                await next(context);
                await transaction.CommitAsync();
            }
            catch
            {
                if (transaction != null)
                    await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                transaction?.Dispose();
            }
        }
    }

    internal static class DbTransactionMiddlewareExtension
    {
        internal static IApplicationBuilder UseDbTransaction(this IApplicationBuilder app)
        {
            app.UseMiddleware<DbTransactionMiddleware>();
            return app;
        }
    }
}
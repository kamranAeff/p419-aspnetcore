using System.Diagnostics;

namespace WebIntroEmpty2.AppCode.Middlewares
{
    public class StopwatchMiddleware
    {
        private readonly RequestDelegate next;

        public StopwatchMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            context.Response.OnStarting((object ctx) =>
            {

                stopwatch.Stop();

                if (ctx is HttpContext c)
                {
                    c.Response.Headers.Add("Elapsed", $"{stopwatch.ElapsedMilliseconds}ms");
                }

                return Task.CompletedTask;

            }, context);


            //await next(context);
        }
    }

    public static class StopwatchMiddlewareExtensions
    {
        public static IApplicationBuilder UseStopwatch(this IApplicationBuilder app)
        {
            app.UseMiddleware<StopwatchMiddleware>();
            return app;
        }
    }
}

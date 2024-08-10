using MediatR;
using System.Diagnostics;

namespace Application.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();
            sw.Start();
            var response = await next();
            sw.Stop();

            Console.WriteLine($"{request.GetType().Name} execute during {sw.ElapsedMilliseconds}ms");
            return response;
        }
    }
}

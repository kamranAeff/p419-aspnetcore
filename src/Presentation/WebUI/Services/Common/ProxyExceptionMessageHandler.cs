using Newtonsoft.Json;
using System.Net;
using WebUI.Exceptions;
using WebUI.Models.Common;

namespace WebUI.Services.Common
{
    public class ProxyExceptionMessageHandler : DelegatingHandler
    {
        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
                return response;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized when (!request.Headers.TryGetValues("raise401", out IEnumerable<string> raiseError) || !raiseError.Any()):
                    return response;
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException();
                case HttpStatusCode.NotFound:
                    throw new NotFoundException();
                case HttpStatusCode.BadRequest:
                    var badRequestJson = await response.Content.ReadAsStringAsync(cancellationToken);
                    var badResponse = JsonConvert.DeserializeObject<ApiResponse>(badRequestJson);
                    throw new BadRequestException("BADREQ", badResponse.Errors);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

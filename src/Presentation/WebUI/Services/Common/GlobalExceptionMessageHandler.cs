using Newtonsoft.Json;
using System.Net;
using WebUI.Models.Common;

namespace WebUI.Services.Common
{
    public class GlobalExceptionMessageHandler : DelegatingHandler
    {
        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
                return response;

#warning Bu Hisse RefreshToken mentiqini block edir duzeltmek lazimdir
            switch (response.StatusCode)
            {
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


    public class BadRequestException : Exception
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; private set; }

        public BadRequestException(string message, Dictionary<string, IEnumerable<string>> errors)
            : base(message) => this.Errors = errors;
    }

    public class NotFoundException : Exception
    {
    }
}

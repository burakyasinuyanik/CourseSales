
using CourseSales.Web.Services;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Http;

namespace CourseSales.Web.DelegateHandlers
{
    public class ClientAuthenticatedHttpClientHandler(
        IHttpContextAccessor httpContextAccessor,
          TokenService tokenService) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                return await base.SendAsync(request, cancellationToken);
            }
            var user = httpContextAccessor.HttpContext.User;
            if (user.Identity!.IsAuthenticated)
            {
                return await base.SendAsync(request, cancellationToken);

            }
            var tokenResponse = await tokenService.GetClientAccessToken();
            if (tokenResponse.IsError) { throw new UnauthorizedAccessException($"{tokenResponse.Error}"); }

            request.SetBearerToken(tokenResponse.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

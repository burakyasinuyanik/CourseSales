using CourseSales.Web.Services;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;

namespace CourseSales.Web.DelegateHandlers
{
    public class AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor,
        TokenService tokenService) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                return await base.SendAsync(request, cancellationToken);
            }
            var user= httpContextAccessor.HttpContext.User;
            if (!user.Identity!.IsAuthenticated)
            {
                return await base.SendAsync(request, cancellationToken);

            }
            var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
               throw new UnauthorizedAccessException("Access token is null or empty");
            }
            
            request.SetBearerToken(accessToken);

            var response = await base.SendAsync(request, cancellationToken);

            if(response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return response;
            }
            var refreshToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new UnauthorizedAccessException("No Refresh token is null or empty");

            }
            var tokenResponse= await tokenService.GetTokensByRefreshToken(refreshToken);
            if (tokenResponse.IsError)
            {
                throw new UnauthorizedAccessException($"{tokenResponse.Error}");
                   
            }
            var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);
            var userClaim=httpContextAccessor.HttpContext.User.Claims;
            var claimIdentity = new ClaimsIdentity(userClaim, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name,ClaimTypes.Role);
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            request.SetBearerToken(tokenResponse.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

using CourseSales.Web.Options;
using CourseSales.Web.Pages.Auth.SignUp;
using CourseSales.Web.Services;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CourseSales.Web.Pages.Auth.SignIn
{
    public class SignInService(IdentityOption identityOption,
        HttpClient httpClient,
        ILogger<SignInService> logger,
        TokenService tokenService,
        IHttpContextAccessor httpContextAccessor)
    {

        public async Task<ServiceResult> AuthenticateAsync(SignInViewModel signInViewModel)
        {
            var tokenResponse = await GetClientAccessToken(signInViewModel);
            if (tokenResponse.IsError)
            {

                return ServiceResult.Error(tokenResponse.Error, tokenResponse.ErrorDescription);
            }
            var userClaims = tokenService.ExtractClaims(tokenResponse.AccessToken!);
            var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);
            var claimIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);


            return ServiceResult.Success();
        }

        private async Task<TokenResponse> GetClientAccessToken(SignInViewModel signInViewModel)
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Admin.Address,
                Policy =
                {
                    RequireHttps = false
                }
            };
            httpClient.BaseAddress = new Uri(identityOption.Admin.Address);
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync();
            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                UserName = signInViewModel.Email!,
                Password = signInViewModel.Password,

            });

            return tokenResponse!;

        }
    }
}

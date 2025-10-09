using CourseSales.Web.Options;
using CourseSales.Web.Pages.Auth.SignUp;
using Duende.IdentityModel.Client;

namespace CourseSales.Web.Pages.Auth.SignIn
{
    public class SignInService(IdentityOption identityOption,
        HttpClient httpClient,
        ILogger<SignInService> logger)
    {


        private async Task<string> GetClientAccessToken(SignInViewModel signInViewModel)
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
            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }
            return tokenResponse.AccessToken!;

        }
    }
}

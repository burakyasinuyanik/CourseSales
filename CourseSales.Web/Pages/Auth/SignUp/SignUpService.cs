using CourseSales.Web.Options;
using CourseSales.Web.Services;
using Duende.IdentityModel.Client;
using System.Linq.Expressions;
using System.Net;

namespace CourseSales.Web.Pages.Auth.SignUp
{
    public record KeyCloakErrorResponse(string ErrorMessage
    );
    public class SignUpService(
        IdentityOption identityOption,
        HttpClient httpClient,
        ILogger<SignUpService> logger
        )
    {
        private static UserCreateRequest ToUserCreateRequest(SignUpViewModel model)
        {
            return new UserCreateRequest(
                Username: model.UserName,
                Enabled: true,
                FirstName: model.FirstName,
                LastName: model.LastName,
                Email: model.Email,
                Credentials: new List<Credential>()
                {
                    new Credential(
                        Type: "password",
                        Value: model.Password,
                        Temporary: false
                    )
                }
            );
        }
        public async Task<ServiceResult> CreateAccount(SignUpViewModel model)
        
        {
            var token = await GetClientCredentialTokenAsAdmin();
            var address = $"{identityOption.BaseAddress}/admin/realms/CourseSalesTenant/users";
            httpClient.SetBearerToken(token);
            var userCreateRequest = ToUserCreateRequest(model);
            var response = await httpClient.PostAsJsonAsync(address,userCreateRequest);
            if(!response.IsSuccessStatusCode)
            {
                if (response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    var keyCloakError = await response.Content.ReadFromJsonAsync<KeyCloakErrorResponse>();
                    logger.LogError("User create error: {error}", keyCloakError!.ErrorMessage);
                    return ServiceResult.Error(keyCloakError?.ErrorMessage);
                }
                var error = await response.Content.ReadAsStringAsync();
                logger.LogError("User create error: {error}",error);
                return ServiceResult.Error("Sistemsel bir hata","Lütfen daha sonra tekrar deneyiniz.");
            }
            return ServiceResult.Success();
        }
        private async Task<string> GetClientCredentialTokenAsAdmin()
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.BaseAddress,
                Policy =
                {
                    RequireHttps = false
                }
            };
            httpClient.BaseAddress = new Uri(identityOption.BaseAddress);
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync();
            if(discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret,
                
            });
            if(tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }
            return tokenResponse.AccessToken!;

        }
    }
}

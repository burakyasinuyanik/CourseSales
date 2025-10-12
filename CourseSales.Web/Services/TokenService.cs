using CourseSales.Web.Options;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace CourseSales.Web.Services
{
    public class TokenService(HttpClient httpClient, IdentityOption identityOption)
    {
        public List<Claim> ExtractClaims(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtWebToken = handler.ReadJwtToken(accessToken);
            return jwtWebToken.Claims.ToList();
        }
        public AuthenticationProperties CreateAuthenticationProperties(TokenResponse tokenResponse)
        {
            var authenticationTokens = new List<AuthenticationToken>
            {
                new()
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value=tokenResponse.AccessToken!
                },
                new()
                {
                     Name=OpenIdConnectParameterNames.RefreshToken,
                     Value=tokenResponse.RefreshToken!
                },
                new()
                {
                     Name=OpenIdConnectParameterNames.ExpiresIn,
                     Value=DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToString("o")
                },

            };
            AuthenticationProperties authenticationProperties = new();
            authenticationProperties.IsPersistent = true;
            authenticationProperties.StoreTokens(authenticationTokens);
            return authenticationProperties;

        }

        public async Task<TokenResponse> GetTokenByRefreshToken(string refreshToken)
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
            var tokenResponse = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = identityOption.Admin.Address,
                ClientId=identityOption.Admin.ClientId,
                ClientSecret=identityOption.Admin.ClientSecret,
                RefreshToken=refreshToken
            });
            return tokenResponse;
        }

        public async Task<TokenResponse> GetTokenByClientCredentials()
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
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = identityOption.Admin.Address,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret,
               
            });
            return tokenResponse;
        }
    }

}

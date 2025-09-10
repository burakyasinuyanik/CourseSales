using CourseSales.Shared.Options;
using Duende.IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Contracts.Refit
{
    public class ClientAuthenticatedHttpClientHandler(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory) : DelegatingHandler
    {
        override protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(request.Headers.Authorization is not null)
                return await base.SendAsync(request, cancellationToken);
            using var scope = serviceProvider.CreateScope();
            var identityOption = scope.ServiceProvider.GetRequiredService<IdentityOption>();
            var clientSecretOption = scope.ServiceProvider.GetRequiredService<ClientSecretOption>();

            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };
            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync();
            if (discoveryResponse.IsError)
                throw new Exception($"Discovery hata{discoveryResponse.Error}");

            var tokenReponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = clientSecretOption.Id,
                ClientSecret = clientSecretOption.ClientSecret,
               

            });

            if(tokenReponse.IsError)
                throw new Exception($"Token hata{tokenReponse.Error}");

            request.SetBearerToken(tokenReponse.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

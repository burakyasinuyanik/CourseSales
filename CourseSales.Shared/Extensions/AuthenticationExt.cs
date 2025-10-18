using CourseSales.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CourseSales.Shared.Extensions
{
    public static class AuthenticationExt
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
        {
           var identityOption= configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();
            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = identityOption!.Address;
                    options.Audience = identityOption.Audience;
                    options.RequireHttpsMetadata = false;
                    

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,

                        ValidateAudience = true,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        RoleClaimType = ClaimTypes.Role,

                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                })
                .AddJwtBearer("ClientCredentialSchema", options =>
                {
                    options.Authority = identityOption!.Address;
                    options.Audience = identityOption.Audience;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,

                        ValidateAudience = true,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true
                    };
                });


            services.AddAuthorization(options =>{
                options.AddPolicy("ClientCredential", policy =>
                {
                    policy.AuthenticationSchemes.Add("ClientCredentialSchema");
                    policy.RequireAuthenticatedUser();
                    
                });
                options.AddPolicy("Password", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);

                });
                options.AddPolicy("instructor", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);
                    policy.RequireRole(ClaimTypes.Role,"instructor");

                });

            });
            return services;
        }
    }
}

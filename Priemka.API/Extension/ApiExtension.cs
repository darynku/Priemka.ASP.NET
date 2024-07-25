using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Priemka.API.Authorization;
using Priemka.Infrastructure.Constants;
using Priemka.Infrastructure.Options;
using System.Text;
namespace Priemka.API.Extension
{
    public static class ApiExtension
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var key = configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>()
                                                    ?? throw new ApplicationException("Wrong configuration");

                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.SecretKey));

                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricKey
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["cookie"];
                        return Task.CompletedTask;
                    }
                };
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("DoctorPolicy", policy =>
                {
                    policy.RequireClaim(Constants.Authentication.Role, "Doctor");
 
                });
            });
        }
    }
}

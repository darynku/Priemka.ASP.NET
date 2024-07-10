using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Priemka.API.Authorization;
using Priemka.Infrastructure.Options;
using System.Text;
namespace Priemka.API.Extension
{
    public static class ApiExtension
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionPolicyHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>()
                                ?? throw new ApplicationException("JwtOptions не найдены");
                    var symmetrictKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.SecretKey));
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = symmetrictKey
                    };
                });

            services.AddAuthorization();
        }
    }
}

using FluentResults;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using Priemka.Infrastructure.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Priemka.Infrastructure.Providers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public Result<string> Generate(UserEntity user)
        {
            var jwtHandler = new JsonWebTokenHandler();

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var permissionClaims = user.Role.Permissions
                .Select(p => new Claim(Constants.Constants.Authentication.Permissions, p));

            var claims = permissionClaims.Concat(
            [
                new(Constants.Constants.Authentication.UserId, user.Id.ToString()),
                new(Constants.Constants.Authentication.Role, user.Role.Name)
            ]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new(claims),
                SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.Expires)
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);

            if (token is null)
                return Result.Fail("Не удалось создать пользователя");

            return Result.Ok(token);
        }
    }
}

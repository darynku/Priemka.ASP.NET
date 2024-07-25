using Microsoft.AspNetCore.Authorization;
using Priemka.Infrastructure.Constants;

namespace Priemka.API.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAttribute>
    {
        private readonly ILogger<PermissionAuthorizationHandler> _logger;

        public PermissionAuthorizationHandler(ILogger<PermissionAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAttribute requirement)
        {
            var permission = context
                .User.Claims
                .Where(c => c.Type == Constants.Authentication.Permissions)
                .Select(c => c.Value)
                .ToHashSet();

            if(!permission.Contains(requirement.Permissions))
            {
                _logger.LogError($"Пользователь не имеет разрешений: {requirement.Permissions}", requirement.Permissions);
                return Task.CompletedTask;
            }

            _logger.LogInformation($"Пользователь имеет разрешения: {requirement.Permissions}", requirement.Permissions);
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;

namespace Priemka.API.Authorization
{
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public string Permissions { get; }

        public PermissionAttribute(string permissions) : base(policy: permissions) 
        {
            Permissions = permissions; 
        }
    }
}

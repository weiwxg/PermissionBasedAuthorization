using Microsoft.AspNetCore.Authorization;
using Web.Infrastructures.Authorizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        public PermissionAuthorizationHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User.HasPermission(requirement.PermissionName))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

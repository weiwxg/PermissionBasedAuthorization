using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Infrastructures.Authorizations.Extensions;

namespace Web.Infrastructures.Authorizations
{
    public class PermissionChecker : IPermissionChecker
    {
        public bool Check(ClaimsPrincipal principal, string permissionName)
        {
            return principal.IsAdministrator() || principal.HasPermission(permissionName);
        }
    }
}

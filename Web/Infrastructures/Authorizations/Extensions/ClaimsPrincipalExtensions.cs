using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static IEnumerable<string> GetPermissions(this ClaimsPrincipal principal)
        {
            return principal.GetPermissionClaims()?.Select(s => s.Value);
        }

        public static IEnumerable<Claim> GetPermissionClaims(this ClaimsPrincipal principal)
        {
            return principal.FindAll(PermissionClaimType.ClaimType);
        }

        public static bool HasPermission(this ClaimsPrincipal principal, string permissionName)
        {
            if (principal is null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var userPermissionNames = principal.GetPermissions();
            return userPermissionNames != null && userPermissionNames.Any(p => p == permissionName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Web.Data;

namespace Web.Infrastructures.Authorizations
{
    public class PermissionClaimsProducer : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        public PermissionClaimsProducer(UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager
            , IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var roles = identity.Claims.Where(p => p.Type == ClaimTypes.Role).Select(s => s.Value);
            var permissions = await RoleManager.Roles
                                        .Where(p => roles.Contains(p.Name))
                                        .AsNoTracking()
                                        .SelectMany(s => s.RolePermissions)
                                        .Select(s => s.Permission)
                                        .Distinct()
                                        .ToListAsync();
            if (permissions.Any())
            {
                identity.AddClaims(permissions.Select(s => new Claim(PermissionClaimType.ClaimType, s.Name)));
            }
            return identity;
        }
    }
}

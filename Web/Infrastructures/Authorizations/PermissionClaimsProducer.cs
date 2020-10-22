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
    public class PermissionClaimsProducer : UserClaimsPrincipalFactory<User, Role>
    {
        public PermissionClaimsProducer(UserManager<User> userManager
            , RoleManager<Role> roleManager
            , IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var roleNames = identity.FindAll(ClaimTypes.Role).Select(c => c.Value);
            var rolePermissions = await RoleManager.Roles
                .Where(r => roleNames.Contains(r.Name))
                .Include(r => r.RolePermissions)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission)
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
            var userPermissions = (await UserManager.Users
                .Include(u => u.UserPermissions)
                .Where(x => x.Id == user.Id)
                .AsNoTracking()
                .SingleAsync())
                .UserPermissions
                .Select(up => up.Permission)
                .ToList();
            identity.AddClaims((rolePermissions.Union(userPermissions).Select(p => new Claim(PermissionClaimType.ClaimType, p.Name))));
            return identity;
        }
    }
}

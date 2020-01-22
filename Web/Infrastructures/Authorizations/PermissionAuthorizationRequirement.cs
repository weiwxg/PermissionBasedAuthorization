using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }

        public PermissionAuthorizationRequirement(string permissionName)
        {
            PermissionName = permissionName ?? throw new ArgumentNullException(nameof(permissionName));
        }
    }
}

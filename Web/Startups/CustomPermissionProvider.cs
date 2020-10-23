using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Infrastructures.Authorizations;

namespace Web.Startups
{
    public class CustomPermissionProvider : PermissionProvider
    {
        public CustomPermissionProvider(IPermissionManager permissionManager)
            : base(permissionManager)
        {
        }

        public override void Initialize()
        {
            Manager.AddGroup(
                new PermissionGroup(PermissionGroupNames.ROLE)
                    .AddPermission(PermissionNames.VISIT_ROLE)
                    .AddPermission(PermissionNames.CREATE_ROLE)
                    .AddPermission(PermissionNames.EDIT_ROLE)
                    .AddPermission(PermissionNames.DELETE_ROLE)
            ).AddGroup(
                new PermissionGroup(PermissionGroupNames.USER)
                    .AddPermission(PermissionNames.VISIT_USER)
                    .AddPermission(PermissionNames.CREATE_USER)
                    .AddPermission(PermissionNames.EDIT_USER)
                    .AddPermission(PermissionNames.AUTH_USER)
                    .AddPermission(PermissionNames.DELETE_USER)
            );
        }
    }
}

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
            );
        }
    }
}

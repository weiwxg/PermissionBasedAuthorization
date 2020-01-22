using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class PermissionProvider : IPermissionProvider
    {
        protected IPermissionManager Manager { get; }

        public PermissionProvider(IPermissionManager permissionManager)
        {
            Manager = permissionManager;
        }

        public virtual void Initialize()
        {

        }
    }
}

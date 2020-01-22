using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public interface IPermissionManager
    {
        IList<PermissionGroup> PermissionGroups { get; }

        IPermissionManager AddGroup(PermissionGroup group);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public interface IPermissionChecker
    {
        bool Check(ClaimsPrincipal principal, string permissionName);
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Data
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {
            RolePermissions = new List<RolePermission>();
        }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Data
{
    public class Role : IdentityRole
    {
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}

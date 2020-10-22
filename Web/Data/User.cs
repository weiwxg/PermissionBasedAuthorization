using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Data
{
    public class User : IdentityUser
    {
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}

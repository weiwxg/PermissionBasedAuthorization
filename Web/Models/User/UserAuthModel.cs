using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.User
{
    public class UserAuthModel
    {
        public string Id { get; set; }
        public PermissionCheckItem[] Permissions { get; set; } = new PermissionCheckItem[0];
    }
}

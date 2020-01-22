using Web.Infrastructures.Authorizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Data
{
    public class RolePermission
    {
        public string Id { get; set; }

        public Permission Permission { get; set; }
    }
}

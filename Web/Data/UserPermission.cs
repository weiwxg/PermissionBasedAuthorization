using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Infrastructures.Authorizations;

namespace Web.Data
{
    public class UserPermission
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Permission Permission { get; set; }
    }
}

using Web.Infrastructures.Authorizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class RolePermission
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Permission Permission { get; set; }
    }
}

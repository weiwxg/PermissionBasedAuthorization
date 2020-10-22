using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class PermissionGroup
    {
        public PermissionGroup(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; private set; }

        public ICollection<Permission> Permissions { get; private set; } = new HashSet<Permission>();

        public PermissionGroup AddPermission(string name)
        {
            Permissions.Add(new Permission(Name, name));
            return this;
        }
    }
}

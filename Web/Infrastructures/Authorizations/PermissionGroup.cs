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
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
            Permissions = new List<Permission>();
        }

        public string Name { get; private set; }

        public IList<Permission> Permissions { get; private set; }

        public PermissionGroup AddPermission(string name)
        {
            Permissions.Add(new Permission(Name, name));
            return this;
        }

        public PermissionGroup AddPermission(string name, string displayName)
        {
            Permissions.Add(new Permission(name, displayName));
            return this;
        }
    }
}

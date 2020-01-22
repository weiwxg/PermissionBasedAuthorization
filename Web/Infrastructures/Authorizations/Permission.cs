using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class Permission
    {
        public Permission(string group, string name, string displayName = null)
        {
            if (string.IsNullOrEmpty(group))
                throw new ArgumentNullException(nameof(group));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            Group = group;
            Name = name;
            DisplayName = displayName ?? name;
        }

        public string Group { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}

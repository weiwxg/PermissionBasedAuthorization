using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class Permission
    {
        public Permission(string name, string displayName = null)
        {
            Name = name;
            DisplayName = displayName ?? name;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}

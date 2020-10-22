using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class Permission
    {
        public Permission(string group, string name)
        {
            Group = group ?? throw new ArgumentNullException(nameof(group));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Group { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Group}-{Name}";
        }
    }
}

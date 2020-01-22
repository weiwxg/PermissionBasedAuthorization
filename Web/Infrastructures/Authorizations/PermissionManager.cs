using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class PermissionManager : IPermissionManager
    {
        public IList<PermissionGroup> PermissionGroups { get; }

        public PermissionManager()
        {
            PermissionGroups = new List<PermissionGroup>();
        }

        public IPermissionManager AddGroup(PermissionGroup group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
            if (PermissionGroups.Any(p => p.Name == group.Name))
                throw new ArgumentException("permission group: {0} repeats.", group.Name);
            PermissionGroups.Add(group);
            return this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public interface IPermissionProvider
    {
        void Initialize();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Navigations
{
    public interface INavigationProvider
    {
        INavigationManager Manager { get; }

        void Initialize();
    }
}

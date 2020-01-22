using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Navigations
{
    public class NavigationProvider : INavigationProvider
    {
        protected INavigationManager Manager { get; }

        public NavigationProvider(INavigationManager manager)
        {
            Manager = manager;
        }

        public virtual void Initialize() { }
    }
}

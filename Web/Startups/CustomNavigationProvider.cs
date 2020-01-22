using Web.Infrastructures.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Startups
{
    public class CustomNavigationProvider : NavigationProvider
    {
        public CustomNavigationProvider(INavigationManager manager)
            : base(manager)
        {
        }

        public override void Initialize()
        {
            Manager.AddItem(
                new MenuItem(
                    name: "Home",
                    url: "/Home/Index",
                    icon: null,
                    target: Targets.SELF
                )
            ).AddItem(
                new MenuItem(
                    name: "Privacy",
                    url: "/Home/Index",
                    icon: null,
                    target: Targets.SELF,
                    requiredPermission: PermissionNames.VISIT_PRIVACY
                )
            );
        }
    }
}

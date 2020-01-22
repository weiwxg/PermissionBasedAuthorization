using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Navigations
{
    public class NavigationManager : INavigationManager
    {
        public NavigationManager()
        {
            MainMenu = new MenuGroup(MenuGroups.MainMenu);
        }

        public MenuGroup MainMenu { get; }

        public MenuGroup AddItem(MenuItem item)
        {
            if (MainMenu.Items.Any(p => p.Name == item.Name))
                throw new ArgumentException("Menu name repeats: {0}.", item.Name);
            return MainMenu.AddItem(item);
        }
    }
}

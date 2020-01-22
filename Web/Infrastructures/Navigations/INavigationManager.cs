using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Navigations
{
    public interface INavigationManager
    {
        MenuGroup MainMenu { get; }
        MenuGroup AddItem(MenuItem item);
    }
}

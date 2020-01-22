using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Navigations
{
    public class MenuGroup
    {
        public MenuGroup(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
            Items = new List<MenuItem>();
        }

        public string Name { get; }

        public IList<MenuItem> Items { get; }

        public MenuGroup AddItem(MenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Items.Add(item);
            return this;
        }
    }
}

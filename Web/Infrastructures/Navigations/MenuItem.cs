using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Navigations
{
    public class MenuItem
    {
        public MenuItem(string name, string url, string icon, string target)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            Name = name;
            Url = url;
            Icon = icon;
            Target = target;
            Children = new List<MenuItem>();
        }

        public MenuItem(string name, string url, string icon, string target, string requiredPermission)
            : this(name, url, icon, target)
        {
            RequiredPermission = requiredPermission;
        }

        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Icon { get; private set; }
        public string Target { get; private set; }
        public string RequiredPermission { get; set; }
        public IList<MenuItem> Children { get; private set; }

        public MenuItem AddItem(MenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (Children.Any(p => p.Name == item.Name))
                throw new ArgumentException("Menu name repeats: {0}.", item.Name);
            Children.Add(item);
            return this;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PermissionCheckItem
    {
        public string Group { get; set; }

        public string Name { get; set; }

        public bool Checked { get; set; } = false;

        public bool Disabled { get; set; } = false;
    }
}

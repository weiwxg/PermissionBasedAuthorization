using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Role
{
    public class RoleCreateModel
    {
        [Display(Name = "名称")]
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
    }
}

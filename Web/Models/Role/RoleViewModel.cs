using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Role
{
    public class RoleViewModel
    {
        [Display(Name = "Id")]
        [HiddenInput]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Display(Name = "名称")]
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "权限")]
        public PermissionCheckItem[] Permissions { get; set; }
    }

}

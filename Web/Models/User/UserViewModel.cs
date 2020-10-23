using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.User
{
    public class UserViewModel
    {
        [HiddenInput]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Display(Name = "邮箱")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "角色")]
        public RoleCheckItem[] Roles { get; set; } = new RoleCheckItem[0];
    }
}

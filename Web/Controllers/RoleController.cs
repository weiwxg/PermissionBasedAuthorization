using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Infrastructures.Authorizations;
using Web.Models;
using Web.Models.Role;

namespace Web.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IPermissionManager _permissionManager;

        public RoleController(RoleManager<Role> roleManager, IPermissionManager permissionManager)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
        }

        [HttpGet]
        //[HasPermission(PermissionNames.VISIT_ROLE)]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        //[HasPermission(PermissionNames.CREATE_ROLE)]
        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            return View("Edit", new RoleViewModel
            {
                Permissions = _permissionManager.PermissionGroups
                    .SelectMany(x => x.Permissions)
                    .Select(x => new PermissionCheckItem
                    {
                        Group = x.Group,
                        Name = x.Name
                    }).ToArray()
            });
        }

        [HttpGet]
        //[HasPermission(PermissionNames.EDIT_ROLE)]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Title = "Edit";
            var role = await _roleManager.Roles.Include(x => x.RolePermissions).SingleOrDefaultAsync(x => x.Id == id);
            if (role == null)
                return NotFound();
            return View("Edit", new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = _permissionManager.PermissionGroups
                    .SelectMany(x => x.Permissions)
                    .Select(x => new PermissionCheckItem
                    {
                        Group = x.Group,
                        Name = x.Name,
                        Checked = role.RolePermissions.Any(y => y.Permission.Group == x.Group && y.Permission.Name == x.Name)
                    }).ToArray()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[HasPermission(PermissionNames.CREATE_ROLE)]
        public async Task<IActionResult> Save(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.Roles.Include(x => x.RolePermissions).SingleOrDefaultAsync(x => x.Id == model.Id);
                IdentityResult identityResult;
                if (role == null)
                {
                    identityResult = await _roleManager.CreateAsync(new Role
                    {
                        Id = model.Id,
                        Name = model.Name,
                        RolePermissions = model.Permissions.Where(x => x.Checked).Select(x => new RolePermission
                        {
                            Permission = new Permission(x.Group, x.Name)
                        }).ToList()
                    });
                }
                else
                {
                    role.Name = model.Name;
                    role.RolePermissions = model.Permissions.Where(x => x.Checked).Select(x => new RolePermission
                    {
                        Permission = new Permission(x.Group, x.Name)
                    }).ToList();
                    identityResult = await _roleManager.UpdateAsync(role);
                }
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", identityResult.Errors.First().Description);
            }
            return View("Edit", model);
        }

        [HttpGet]
        //[HasPermission(PermissionNames.DELETE_ROLE)]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[HasPermission(PermissionNames.DELETE_ROLE)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.Roles.Include(x => x.RolePermissions).SingleOrDefaultAsync(x => x.Id == id);
            if (role != null)
                await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }
    }
}
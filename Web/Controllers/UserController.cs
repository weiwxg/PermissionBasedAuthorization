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
using Web.Models.User;
using Web.Startups;

namespace Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPermissionManager _permissionManager;

        public UserController(UserManager<User> userManager
            , RoleManager<Role> roleManager
            , IPermissionManager permissionManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionManager = permissionManager;
        }

        [HasPermission(PermissionNames.VISIT_USER)]
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        [HttpGet]
        [HasPermission(PermissionNames.CREATE_USER)]
        public async Task<IActionResult> Create()
        {
            return View(new UserViewModel
            {
                Roles = await _roleManager.Roles.Select(x => new RoleCheckItem { RoleName = x.Name }).ToArrayAsync()
            });
        }

        [HttpPost]
        [HasPermission(PermissionNames.CREATE_USER)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                };
                var rst = await _userManager.CreateAsync(user, "1q2w3E*");
                if (rst.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, model.Roles.Where(x => x.Checked).Select(x => x.RoleName));
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", rst.Errors.First().Description);
            }
            return View(model);
        }

        [HttpGet]
        [HasPermission(PermissionNames.EDIT_USER)]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.Users.Include(x => x.UserPermissions).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return View(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Roles = await _roleManager.Roles.Select(x => new RoleCheckItem
                {
                    RoleName = x.Name,
                    Checked = _userManager.IsInRoleAsync(user, x.Name).Result
                }).ToArrayAsync()
            });
        }

        [HttpPost]
        [HasPermission(PermissionNames.EDIT_USER)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.Include(x => x.UserPermissions).SingleOrDefaultAsync(x => x.Id == model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    var rst = await _userManager.UpdateAsync(user);
                    if (rst.Succeeded)
                    {
                        await _userManager.RemoveFromRolesAsync(user, model.Roles.Where(x => !x.Checked).Select(x => x.RoleName));
                        await _userManager.AddToRolesAsync(user, model.Roles.Where(x => x.Checked).Select(x => x.RoleName));
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", rst.Errors.First().Description);
                }
                ModelState.AddModelError("", "当前用户不存在");
            }
            return View(model);
        }

        [HttpGet]
        [HasPermission(PermissionNames.AUTH_USER)]
        public async Task<IActionResult> Auth(string id)
        {
            var user = await _userManager.Users.Include(x => x.UserPermissions).Where(x => x.Id == id).SingleOrDefaultAsync();
            if (user == null)
                return NotFound();
            var roleNames = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.Include(x => x.RolePermissions).AsNoTracking().Where(x => roleNames.Contains(x.Name)).ToListAsync();
            var userPermissions = user.UserPermissions.Select(x => x.Permission).Distinct();
            var rolePermissions = roles.SelectMany(x => x.RolePermissions).Select(x => x.Permission).Distinct();
            return View(new UserAuthModel
            {
                Id = id,
                Permissions = _permissionManager.PermissionGroups
                    .SelectMany(x => x.Permissions)
                    .Select(x => new PermissionCheckItem
                    {
                        Group = x.Group,
                        Name = x.Name,
                        Checked = userPermissions.Any(up => up.Group == x.Group && up.Name == x.Name) || rolePermissions.Any(rp => rp.Group == x.Group && rp.Name == x.Name),
                        Disabled = rolePermissions.Any(rp => rp.Group == x.Group && rp.Name == x.Name)
                    }).ToArray()
            });
        }

        [HttpPost]
        [HasPermission(PermissionNames.AUTH_USER)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Auth(UserAuthModel model)
        {
            var user = await _userManager.Users.Include(x => x.UserPermissions).SingleOrDefaultAsync(x => x.Id == model.Id);
            if (user == null)
                return NotFound();
            user.UserPermissions = model.Permissions
                .Where(x => !x.Disabled && x.Checked)
                .Select(x => new UserPermission
                {
                    Permission = new Permission(x.Group, x.Name)
                }).ToList();
            var rst = await _userManager.UpdateAsync(user);
            if (rst.Succeeded)
                return RedirectToAction("Index");
            ModelState.AddModelError("", rst.Errors.First().Description);
            return View(model);
        }

        [HttpGet]
        [HasPermission(PermissionNames.DELETE_USER)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(PermissionNames.DELETE_USER)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.Users.Include(x => x.UserPermissions).SingleOrDefaultAsync(x => x.Id == id);
            if (user != null)
                await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}

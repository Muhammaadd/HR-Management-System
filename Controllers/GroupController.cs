using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRSystem.Controllers
{
    public class GroupController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IGroupService groupService;

        public GroupController(RoleManager<IdentityRole> roleManager, IGroupService groupService)
        {
            this.roleManager = roleManager;
            this.groupService = groupService;
        }
        [Authorize(Permissions.Permission.View)]

        public IActionResult Index()
        {
            return View(groupService.GetRoles());
        }
        [Authorize(Permissions.Permission.Create)]

        public IActionResult AddNewGroup()
        {
            var allClaims = Permissions.GenerateAllPermissions();
            var allPermissions = allClaims.Select(p => new CheckBoxViewModel { DisplayValue = p }).ToList();
            var viewModel = new PermissionsFormViewModel
            {
                RoleId = "",
                RoleName = "",
                RoleCalims = allPermissions
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permission.Create)]

        public async Task<IActionResult> AddNewGroup(PermissionsFormViewModel model)
        {
            int counter = 0;
            foreach (var item in model.RoleCalims)
            {
                if (!item.IsSelected)
                    counter++;
            }
            if (counter == 28)
            {
                ModelState.AddModelError("RoleCalims", "Please select the permissions");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await roleManager.CreateAsync(new IdentityRole { Name = model.RoleName.Trim() });
            IdentityRole newGroup = await roleManager.FindByNameAsync(model.RoleName);
            var selectedClaims = model.RoleCalims.Where(c => c.IsSelected).ToList();
            foreach (var claim in selectedClaims)
                await roleManager.AddClaimAsync(newGroup, new Claim("Permission", claim.DisplayValue));
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Permissions.Permission.Edit)]

        public async Task<IActionResult> Edit(string GroupId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(GroupId);
            if (role == null)
                return NotFound();
            var allClaims = Permissions.GenerateAllPermissions();
            var allPermissions = allClaims.Select(p => new CheckBoxViewModel { DisplayValue = p }).ToList();
            var roleClaims = roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
            foreach (var permission in allPermissions)
            {
                if (roleClaims.Any(c => c == permission.DisplayValue))
                    permission.IsSelected = true;
            }
            var viewModel = new PermissionsFormViewModel
            {
                RoleId = GroupId,
                RoleName = role.Name,
                RoleCalims = allPermissions
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permission.Edit)]

        public async Task<IActionResult> Edit(PermissionsFormViewModel model)
        {
            IdentityRole role = await roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
                return NotFound();
            int counter = 0;
            foreach (var item in model.RoleCalims)
            {
                if (!item.IsSelected)
                    counter++;
            }
            if (counter == 28)
            {
                ModelState.AddModelError("RoleCalims", "Please select the permissions");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var groupClaims = await roleManager.GetClaimsAsync(role);
            foreach (var claim in groupClaims)
                await roleManager.RemoveClaimAsync(role, claim);
            var selectedPermission = model.RoleCalims.Where(p => p.IsSelected).ToList();
            foreach (var permission in selectedPermission)
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission.DisplayValue));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Permissions.Permission.Delete)]

        public async Task<IActionResult> Delete(string GroupId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(GroupId);
            groupService.Delete(role);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UniqueGroupName(string RoleName, string RoleId)
        {
            var role = await roleManager.FindByNameAsync(RoleName);
            if (role != null && role.Id != RoleId)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}

using GameZone.Areas.Admin.ViewModels;
using GameZone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var usersList = await _userManager.Users.ToListAsync();

            var users = new List<UserVM>();

            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);

                users.Add(new UserVM
                {
                    Id = user.Id,
                    FullName = user.FisrtName + " " + user.LastName,
                    Emile = user.Email,
                    Role = roles.FirstOrDefault()
                });
            }

            return View(users);
        }

        public async Task<IActionResult> Create()
        {
            var CreareUserVM = new CreateUserVM
            {
                Roles = await GetRolesAsync()
            };
            return View(CreareUserVM);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = await GetRolesAsync();
                return View(model);
            }

            if (model.RoleId == "Select Role")
            {
                ModelState.AddModelError("RoleId", "Please select a role");
                model.Roles = await GetRolesAsync();
                return View(model);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                model.Roles = await GetRolesAsync();
                return View(model);
            }

            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email,
                FisrtName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                model.Roles = await GetRolesAsync();
                return View(model);
            }

            await _userManager.AddToRoleAsync(user, (await _roleManager.FindByIdAsync(model.RoleId)).Name);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("User Not Found");

            var roles = await GetRolesAsync();
            var userRoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var userRoleId = roles.FirstOrDefault(r => r.Text == userRoleName)?.Value;

            var CreaeUserVM = new EditUserVM
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FisrtName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Roles = roles,
                RoleId = userRoleId
            };

            return View(CreaeUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = await GetRolesAsync();
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound("User Not Found");
            }

            user.Email = model.Email;
            user.UserName = model.Email;
            user.FisrtName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                model.Roles = await GetRolesAsync();
                return View(model);
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            if (existingRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, existingRoles);
            }

            var newRole = await _roleManager.FindByIdAsync(model.RoleId);
            if (newRole != null)
            {
                await _userManager.AddToRoleAsync(user, newRole.Name);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("User Not Found");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("User not deleted");
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetRolesAsync()
        {
            return await _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).ToListAsync();
        }

    }
}

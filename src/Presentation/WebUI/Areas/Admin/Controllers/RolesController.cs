using Domain.Entities.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController(RoleManager<OganiRole> roleManager) : Controller
    {
        [Authorize("admin.roles.get")]
        public async Task<IActionResult> Index()
        {
            var response = await roleManager.Roles.ToListAsync();
            return View(response);
        }

        [Authorize("admin.roles.get")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(response);
        }

        [Authorize("admin.roles.add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize("admin.roles.add")]
        public async Task<IActionResult> Add(string name)
        {
            var role = new OganiRole();
            role.Name = name;

            await roleManager.CreateAsync(role);
            return RedirectToAction(nameof(Index));
        }

        [Authorize("admin.roles.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(response);
        }

        [HttpPost]
        [Authorize("admin.roles.edit")]
        public async Task<IActionResult> Edit(int id, string name)
        {
            var entity = await roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);

            entity.Name = name;
            entity.NormalizedName = name.ToUpper();
            await roleManager.UpdateAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize("admin.roles.remove")]
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);

            await roleManager.DeleteAsync(entity!);

            return Json(new
            {
                error = false,
                message = "ok"
            });
        }
    }
}

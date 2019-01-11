using ManagerCloud.MVC.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager => 
            HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();

        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await RoleManager.CreateAsync(new ApplicationRole
            {
                Name = model.Name,
                Description = model.Description
            });

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Something went wrong...");
            return View(model);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                return View(new EditRoleModel { Id = role.Id, Name = role.Name, Description = role.Description });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var role = await RoleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return View(model);
            }

            role.Description = model.Description;
            role.Name = model.Name;
            var result = await RoleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Something went wrong...");
            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await RoleManager.DeleteAsync(role);
            }

            return RedirectToAction("Index");
        }
    }
}
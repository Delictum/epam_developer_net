using ManagerCloud.MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Client = model.Client };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(model);
        }

        private IAuthenticationManager AuthenticationManager => 
            HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    var claim = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Logout", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Edit()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            EditModel model = new EditModel { Client = user.Client };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.Client = model.Client;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong...");
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View(model);
        }
    }
}
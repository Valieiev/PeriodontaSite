using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models.Users;
using PeriodontalSite1.ViewModel;
using PeriodontalSite1.ViewModel.Account;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.Name, LastName = model.Surname, MiddleName = model.Patronymic, Birth = model.Birth, Address = model.Address, Sex = model.Sex, PhoneNumber = model.PhoneNumber };
                IdentityResult result =  UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            if (user != null)
            {

                    return RedirectToAction("Cabinet", "Account");
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user =  UserManager.Find(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim =  UserManager.CreateIdentity(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
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
        public ActionResult DeleteConfirmed()
        {
            ApplicationUser user =  UserManager.FindByEmail(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result =  UserManager.Delete(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit()
        {
            var user =  UserManager.FindByEmail(User.Identity.Name).Map<AccountEditViewModel>();
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Edit(AccountEditViewModel model)
        {
            ApplicationUser user =  UserManager.FindByEmail(User.Identity.Name);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
               // if(model.Birth != Convert.ToDateTime("01.01.0001 0:00:00"))  user.Birth = model.Birth;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                IdentityResult result =  UserManager.Update(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }
        public ActionResult Cabinet()
        {
            ApplicationUser user =  UserManager.FindByEmail(User.Identity.Name);
            if (user != null)
            {
                CabinetViewModel model = new CabinetViewModel { FirstName = user.FirstName, LastName = user.LastName, Birth = user.Birth, MiddleName= user.MiddleName, Sex = user.Sex, PhoneNumber = user.PhoneNumber, Address = user.Address };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}
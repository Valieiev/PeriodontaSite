using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PeriodontalSite1.Models.Account;
using PeriodontalSite1.Models.Users;
using PeriodontalSite1.ViewModel.Role;
using PeriodontalSite1.ViewModel.Roles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Index()
        {

            return View(RoleManager.Roles);
        }
      

        public ActionResult Edit(string id)
        {

            string roleName = RoleManager.FindById(id).Name;
            var role =  RoleManager.Roles.Single(r => r.Name == roleName);
            var users = UserManager.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id));
            IEnumerable<ApplicationUser> members = users;
            IEnumerable<ApplicationUser> nonMembers = UserManager.Users.Except(members);
            return View(new RoleEditViewModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }
        [HttpPost]
        public ActionResult Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = UserManager.AddToRole(userId, model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = UserManager.RemoveFromRole(userId,
                    model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Index");

            }
            return View("Error", new string[] { "Роль не найдена" });
        }
    
  
    }
}
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models.Account;
using PeriodontalSite1.Models.Users;
using PeriodontalSite1.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;

namespace PeriodontalSite1.Controllers
{
    [Authorize(Roles = "Owner")]
    public class AdminController : Controller 
    {
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var result = UserManager.Users.ToList().Map<List<AdminViewModel>>();

            return View(new AdminViewModel()
            {
                Users = result.ToPagedList(pageNumber, pageSize)
            }
            );
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }


        public ActionResult Edit(string id)
        {

            var user = UserManager.FindById(id);
            var model = Mapper.Map<AdminEditViewModel>(user);
            model.Members = UserManager.GetRoles(id);
            PrepareAdminEditViewModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(AdminEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(model.Id);
                if (user != null)
                {
                    Mapper.Map(model, user);
                    IdentityResult result = UserManager.Update(user);
                    //Role
                    List<string> memberRole = UserManager.GetRoles(model.Id).ToList();

                   
                    if (result.Succeeded)
                    {
                        foreach (var role in memberRole)
                        {
                            if (!model.Members.Contains(role)) UserManager.RemoveFromRole(model.Id, role);
                        }
                        foreach (var role in model.Members)
                        {
                            if (!memberRole.Contains(role)) UserManager.AddToRole(model.Id, role);
                        }
                        return RedirectToAction(nameof(Index), "Admin");
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
            }

            
            PrepareAdminEditViewModel(model);
            return View(model);
        }



        public ActionResult Delete(string id)
        {
            ApplicationUser user =  UserManager.FindById(id);
            if (user != null)
            {
                IdentityResult result =  UserManager.Delete(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index), "Admin");
                }
            }
            return RedirectToAction(nameof(Index), "Admin");
        }


        private void PrepareAdminEditViewModel(AdminEditViewModel model)
        {
            model.Roles = RoleManager.Roles.ToList();
        }


    }
}
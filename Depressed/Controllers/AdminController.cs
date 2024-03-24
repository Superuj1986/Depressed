using Depressed.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Depressed.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: Admin
        [HttpGet]
        public ActionResult List()
        {
            ApplicationRoleManager RoleManager = HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = (from user in UserManager.Users
                         from userRole in user.Roles
                         join role in RoleManager.Roles on userRole.RoleId equals role.Id
                         //where role.Name == "Teacher"
                         select new UserList()
                         {
                             UserId = user.Id,
                             UserName = user.UserName,
                             Email = user.Email,
                             RoleName = role.Name
                         }).ToList();
            return View(users);
        }
        [HttpGet]
        public ActionResult RoleList()
        {
            ApplicationRoleManager RoleManager = HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            var roles = RoleManager.Roles.Select(x => new RoleViewDto() { RoleId = x.Id, Name = x.Name }).ToList();
            return View(roles);
        }
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(RegisterRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole { Name = model.RoleName };
                IdentityResult result = RoleManager.Create(role);
                if (result.Succeeded)
                {
                    string RoleId = role.Id;
                    return RedirectToAction("List", "Admin", new { Id = RoleId });
                }
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleToEdit = RoleManager.FindById(model.RoleId);
                if (roleToEdit == null)
                {
                    return HttpNotFound();
                }
                if (roleToEdit.Name != model.RoleName)
                {
                    roleToEdit.Name = model.RoleName;
                }

                IdentityResult result = RoleManager.Update(roleToEdit);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { Id = model.RoleId });
                }
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteRole(DeleteRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleToDelete = RoleManager.FindById(model.RoleId);
                if (roleToDelete == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result = RoleManager.Delete(roleToDelete);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteUser(string UserId)
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser UserToDelete = UserManager.FindById(UserId);
            if (UserToDelete != null)
            {
                IdentityResult result = UserManager.Delete(UserToDelete);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);
                return View(UserId);
            }
            return HttpNotFound();
        }
    }
}
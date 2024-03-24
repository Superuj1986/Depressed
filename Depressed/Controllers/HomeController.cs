using Depressed.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace Depressed.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;
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
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //User areas
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AddUser()
        {
            ApplicationRoleManager RoleManager = HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            var roles = RoleManager.Roles
                .Where(x=>!x.Name.Equals("Admin"))
                .Select(x => new RoleViewDto() {RoleId=x.Id,Name=x.Name})
                .ToList();

            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(RegisterViewModel model)
        {
            ApplicationRoleManager RoleManager = HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            var roles = RoleManager.Roles
                .Where(x=>!x.Name.Equals("Admin"))
                .Select(x => new RoleViewDto() { RoleId = x.Id, Name = x.Name })
                .ToList();

            ViewBag.Roles = roles;
            
            if (ModelState.IsValid)
            {
                var selectedRole = (await RoleManager.FindByIdAsync(model.RoleId)).Name;
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                };
                var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();


                var result = await UserManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, selectedRole);
                    var SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult UpdateUser(string UserId)
        {
            EditUserViewModel model = new EditUserViewModel();
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser UserToEdit = UserManager.FindById(UserId);
            if ( UserToEdit != null)
            {
                model.UserId = UserToEdit.Id;
                model.UserName = UserToEdit.UserName;
                model.FullName = UserToEdit.Fullname;
                model.Age = UserToEdit.Age;
                model.Birthdate = UserToEdit.BirtDate;
                model.Main_subject = UserToEdit.Main_subject;
                model.address = UserToEdit.Address;
                model.Phone_number = UserToEdit.PhoneNumber;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateUser(EditUserViewModel model/*, HttpPostedFileBase postedFile*/)
        {
            /*if ( postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
            }*/
            if (ModelState.IsValid)
            {
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser UserToEdit = UserManager.FindById(model.UserId);
                if (UserToEdit.UserName != model.UserName)
                    UserToEdit.UserName = model.UserName;
                if (UserToEdit.Fullname != model.FullName)
                    UserToEdit.Fullname = model.FullName;
                if (UserToEdit.Age != model.Age)
                    UserToEdit.Age = model.Age;
                if (UserToEdit.Main_subject != model.Main_subject)
                    UserToEdit.Main_subject = model.Main_subject;
                if (UserToEdit.BirtDate != model.Birthdate)
                    UserToEdit.BirtDate = model.Birthdate;
                if (UserToEdit.Address != model.address)
                    UserToEdit.Address = model.address;
                if (UserToEdit.PhoneNumber != model.Phone_number)
                    UserToEdit.PhoneNumber = model.Phone_number;
                IdentityResult result = UserManager.Update(UserToEdit);
                if ( result.Succeeded)
                {
                    return RedirectToAction("UpdateUser", "Home");
                }
                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Security(string UserId)
        {
            ChangeEmail model = new ChangeEmail();
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser UserToEdit = UserManager.FindById(UserId);
            if ( UserToEdit != null )
            {
                model.UserId = UserToEdit.Id;
                model.Email = UserToEdit.Email;
                ViewBag.Email = UserToEdit.Email;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Security(ChangeEmail model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser UserToEdit = UserManager.FindById(model.UserId);
                if (UserToEdit.Email != model.Email)
                    UserToEdit.Email = model.Email;
                IdentityResult result = UserManager.Update(UserToEdit);
                if (result.Succeeded)
                {
                    return RedirectToAction("Security", "Home");
                }
                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    IdentityResult result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.Password);
                    if (result.Succeeded)
                    {
                        user = UserManager.FindById(User.Identity.GetUserId());
                        if (user != null)
                        {
                            ApplicationSignInManager SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                            SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                        }
                        return RedirectToAction("ChangePassword", "Acount");
                    }
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                return HttpNotFound();
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                bool IsSendEmail = SendEmail.EmailSend(model.Email, "Reset Your Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>", true);
                if (IsSendEmail)
                {
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        
        //Role areas
        [HttpPost]
        public ActionResult AssignUserToRole(string userId, string roleName)
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            IdentityResult result = UserManager.AddToRole(userId, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View();
        }

    }
}
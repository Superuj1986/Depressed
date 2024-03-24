using Depressed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace Depressed.Controllers
{

    public class TeacherController : Controller
    {
        // GET: Teacher
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
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
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Profile(string UserId)
        {
            EditUserViewModel model = new EditUserViewModel();
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser UserToEdit = UserManager.FindById(UserId);
            if (UserToEdit != null)
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
        public ActionResult Profile(EditUserViewModel model)
        {
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
                if (result.Succeeded)
                {
                    return RedirectToAction("UpdateUser", "Home");
                }
                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        public ActionResult RollOut()
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            
            return View();
        }
        [HttpGet]
        public ActionResult Schedule(int id)
        {
            Lichhoc lh = new Lichhoc();
            Lophoc lophoc = db.Lophocs.Where(row => row.class_id == id).FirstOrDefault();
            if ( lophoc != null)
            {
                lh.lophoc_id = lophoc.class_id;
            }
            return View(lh);
        }
        [HttpPost]
        public ActionResult Schedule(Lichhoc lich)
        {
            if ( ModelState.IsValid)
            {
                db.Lichhocs.Add(lich);
                db.SaveChanges();
                return RedirectToAction("Lophoc");
            }
            else
            {
                return RedirectToAction("Schedule");
            }
        }
        [HttpGet]
        public ActionResult EnterMark()
        {
            return View();
        }
        // Classes
        public ActionResult Lophoc(string UserId, string search = "")
        {
            List<Lophoc> lh = db.Lophocs.Where(row => row.UserId == UserId).ToList();
            return View(lh);
        }
        [HttpGet]
        public ActionResult LophocChitiet(int id)
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            List<ClassMember> cm = db.ClassMembers.Where(row => row.lophoc_id == id).ToList();
            ViewBag.lophoc_id = id;
            var lopHoc = db.Lophocs.Include(x => x.Lichhoc).FirstOrDefault(y => y.class_id == id);
            ViewBag.Check = lopHoc.Lichhoc.Any();
            ViewBag.ViewModel = lopHoc;
            return View(cm);
        }
        [HttpGet]
        public ActionResult Add_Lophoc(string UserId)
        {
            Lophoc model = new Lophoc();
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser UserToEdit = UserManager.FindById(UserId);
            if (UserToEdit != null)
            {
                model.UserId = UserToEdit.Id;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Add_Lophoc(Lophoc lop)
        {
            if (ModelState.IsValid)
            {
                db.Lophocs.Add(lop);
                db.SaveChanges();
                return RedirectToAction("Lophoc");
            }
            else
            {
                return RedirectToAction("Add_Lophoc");
            }
        }
        [HttpGet]
        public ActionResult Edit_Lophoc(int id)
        {
            Lophoc lop = db.Lophocs.Where(row => row.class_id == id).FirstOrDefault();
            return View(lop);
        }
        [HttpPost]
        public ActionResult Edit_Lophoc()
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
        //Khoa hoc
        [HttpGet]
        public ActionResult Khoahoc(string UserId, string search = "")
        {
            List<Khoahoc> kh = db.Khoahocs.Where(row => row.UserId == UserId).ToList();
            return View(kh);
        }
        [HttpGet]
        public ActionResult Khoahocct(int id)
        {
            Khoahoc kh = db.Khoahocs.Where(row => row.kh_id == id).FirstOrDefault();
            return View(kh);
        }
        public ActionResult Tao_kh(string UserId)
        {
            ViewBag.Cate = db.Categories.ToList();
            Khoahoc model = new Khoahoc();
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser UserToEdit = UserManager.FindById(UserId);
            if (UserToEdit != null)
            {
                model.UserId = UserToEdit.Id;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Tao_kh(Khoahoc kh)
        {
            if (ModelState.IsValid)
            {
                db.Khoahocs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Khoahoc");
            }
            else
            {
                return RedirectToAction("Tao_kh");
            }
        }
        [HttpGet]
        public ActionResult Edit_kh(int id)
        {
            Khoahoc kh = db.Khoahocs.Where(row => row.kh_id == id).FirstOrDefault();
            return View(kh);
        }
        [HttpPost]
        public ActionResult Edit_kh(Khoahoc kh)
        {
            Khoahoc k = db.Khoahocs.Where(row => row.kh_id == kh.kh_id).FirstOrDefault();
            k.name = kh.name;
            k.category_id = kh.category_id;
            k.Content = kh.Content;
            k.Price = kh.Price;
            db.SaveChanges();
            return View();
        }
        public ActionResult Category(string search = "")
        {
            List<Category> cate = db.Categories.ToList();
            return View(cate);
        }
        public ActionResult NewCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCategory(Category ca)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(ca);
                db.SaveChanges();
                return RedirectToAction("Category");
            }
            else
            {
                return RedirectToAction("NewCategory");
            }
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Blog_dt()
        {
            return View();
        }
    }
}
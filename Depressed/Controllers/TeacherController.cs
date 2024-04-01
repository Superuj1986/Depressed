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
using Depressed.Migrations;
using Lichhoc = Depressed.Models.Lichhoc;
using System.Data.Entity.Infrastructure;

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
            ViewBag.Name = lophoc.class_name;
            if (lophoc != null)
            {
                lh.lophoc_id = lophoc.class_id;
            }
            return View(lh);
        }
        [HttpPost]
        public ActionResult Schedule(Lichhoc lich)
        {
            if (ModelState.IsValid)
            {
                db.Lichhocs.Add(lich);
                db.SaveChanges();
                return RedirectToAction("LophocChitiet", new { id = lich.lophoc_id });
            }
            else
            {
                return RedirectToAction("Schedule");
            }
        }
        [HttpGet]
        public ActionResult Schedule_Fix()
        {
            return View();
        }
        /*[HttpPost]
        public ActionResult Schedule_Fix()
        {

        }*/
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
            ViewBag.lophoc_id = id;
            var classDetailViewModel = new ClassDetailViewModel();
            classDetailViewModel.Lophoc = db.Lophocs.Find(id);
            classDetailViewModel.Lichhocs = db.Lichhocs.Where(x => x.lophoc_id == id).ToList();
            classDetailViewModel.ClassMembers = db.ClassMembers.Where(x => x.lophoc_id == id).ToList();
            return View(classDetailViewModel);
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
                return RedirectToAction("Lophoc", new { UserId = User.Identity.GetUserId() });
            }
            else
            {
                return RedirectToAction("Add_Lophoc");
            }
        }
        [HttpGet]
        public ActionResult Edit_Lophoc(int id)
        {
            var lophoc = db.Lophocs
                .Include(x => x.ClassMembers)
                .Include(y => y.Lichhocs)
                .FirstOrDefault(z => z.class_id == id);
            return View(lophoc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Lophoc(int id, Lophoc lophoc, string[] deleteMemberIds)
        {
            var lophocUpdate = db.Lophocs
                .Include(l => l.Lichhocs)
                .Include(x => x.ClassMembers)
                .FirstOrDefault(y => y.class_id == id);
            lophocUpdate.class_name = lophoc.class_name;
            lophocUpdate.content = lophoc.content;
            var existingLichhocs = lophocUpdate.Lichhocs.ToList();
            foreach (var lichhocView in lophoc.Lichhocs)
            {
                Lichhoc existingLichhoc;
                if (lichhocView.id > 0)
                {
                    existingLichhoc = existingLichhocs.FirstOrDefault(lh => lh.id == lichhocView.id);
                    if (existingLichhoc != null)
                    {
                        existingLichhoc.Ngayhoc1 = lichhocView.Ngayhoc1;
                        existingLichhoc.Tiet_1 = lichhocView.Tiet_1;
                        existingLichhoc.Ngayhoc2 = lichhocView.Ngayhoc2;
                        existingLichhoc.Tiet_2 = lichhocView.Tiet_2;
                        existingLichhoc.Ngayhoc3 = lichhocView.Ngayhoc3;
                        existingLichhoc.Tiet_3 = lichhocView.Tiet_3;
                    }
                }
                else
                {
                    lichhocView.lophoc_id = lophocUpdate.class_id;
                    db.Lichhocs.Add(lichhocView);
                }
            }
            var existingMember = lophocUpdate.ClassMembers.ToList();
            foreach (var memberView in lophoc.ClassMembers)
            {
                if (!existingMember.Any(cm => cm.UserId == memberView.UserId && cm.lophoc_id == lophocUpdate.class_id))
                {
                    ClassMember newMember = new ClassMember();
                    newMember.UserId = memberView.UserId;
                    lophocUpdate.ClassMembers.Add(newMember);
                }
            }
            foreach (var deletedUserId in deleteMemberIds)
            {
                var memberToDelete = existingMember.FirstOrDefault(cm => cm.UserId == deletedUserId);
                if (memberToDelete != null)
                {
                    lophocUpdate.ClassMembers.Remove(memberToDelete);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Lophocct", new { id = lophocUpdate.class_id });
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
                return RedirectToAction("Khoahoc", new { UserId = User.Identity.GetUserId() });
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
            ViewBag.Cate = db.Categories.ToList();
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
            return RedirectToAction("Khoahocct", new { id = kh.kh_id });
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
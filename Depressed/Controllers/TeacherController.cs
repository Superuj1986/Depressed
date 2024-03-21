using Depressed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Depressed.Controllers
{
    
    public class TeacherController : Controller
    {
        // GET: Teacher
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult RollOut(int id)
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult RollOut()
        {

            return View();
        }
        [HttpGet]
        public ActionResult EnterMark()
        {
            return View();
        }
        // Classes
        public ActionResult Lophoc()
        {
            return View();
        }
        public ActionResult LophocChitiet(string id)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add_Lophoc()
        {
            return View();
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
        public ActionResult Edit_Lophoc(int id)
        {
            Lophoc lop = db.Lophocs.Where(row => row.class_id == id).FirstOrDefault();
            return View(lop);
        }

        //Khoa hoc
        [HttpGet]
        public ActionResult Khoahoc()
        {
            return View();
        }
        public ActionResult Tao_kh()
        {
            return View();
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
    }
}
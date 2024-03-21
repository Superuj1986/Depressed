using Depressed.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Depressed.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        /*ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult Profiles()
        {
            return View();
        }
        [HttpPost]
        public void Update(Student model)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(model);
            }
            *//*using ( var context = new ApplicationDbContext())
            {
                context.Students.AddOrUpdate(model);
                context.SaveChanges();
            }*//*
        }
        [HttpGet]
        public void Delete(Student model)
        {
            db.Students.Remove(model);
            db.SaveChanges();
        }*/
    }
}
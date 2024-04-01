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
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
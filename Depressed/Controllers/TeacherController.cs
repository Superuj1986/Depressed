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
    [Authorize(Roles = "Teacher")]
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
        // Classes
        [HttpGet]
        public ActionResult Lophoc(string UserId, string search = "")
        {
            List<Lophoc> lh = db.Lophocs.Where(row => row.UserId == UserId).ToList();
            /*var number = db.Lophocs.Include(x => x.ClassMembers).Where(y => y.class_id).Count();*/
            if (!String.IsNullOrEmpty(search))
            {
                lh = lh.Where(s => s.class_name.Contains(search) && s.UserId == UserId).ToList();
            }
            ViewBag.UserId = UserId;
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
            var lophoc = db.Lophocs.FirstOrDefault(l => l.class_id == id);
            var lichhoc = db.Lichhocs.FirstOrDefault(l => l.lophoc_id == id);
            var viewModel = new EditDetailViewModel
            {
                Lophoc = lophoc,
                Lichhocs = lichhoc,
                ClassMembers = lophoc.ClassMembers.ToList(),
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Lophoc(int id, EditDetailViewModel viewModel, string[] deletedMembers)
        {
            var lophoc = db.Lophocs.FirstOrDefault(l => l.class_id == id);
            var lichhoc = db.Lichhocs.FirstOrDefault(l => l.lophoc_id == id);
            if (ModelState.IsValid)
            {
                lophoc.class_name = viewModel.Lophoc.class_name;
                lophoc.content = viewModel.Lophoc.content;
                lichhoc.Ngayhoc1 = viewModel.Lichhocs.Ngayhoc1;
                lichhoc.Tiet_1 = viewModel.Lichhocs.Tiet_1;
                lichhoc.Ngayhoc2 = viewModel.Lichhocs.Ngayhoc2;
                lichhoc.Tiet_2 = viewModel.Lichhocs.Tiet_2;
                lichhoc.Ngayhoc3 = viewModel.Lichhocs.Ngayhoc3;
                lichhoc.Tiet_3 = viewModel.Lichhocs.Tiet_3;
                if (deletedMembers != null)
                {
                    foreach (var member in deletedMembers)
                    {
                        var memberToDelete = db.ClassMembers.FirstOrDefault(x => x.UserId == member);
                        if (memberToDelete != null)
                        {
                            db.ClassMembers.Remove(memberToDelete);
                        }
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("LophocChitiet", new { id = lophoc.class_id });
        }
        //Khoa hoc
        [HttpGet]
        public ActionResult Khoahoc(string UserId, string search = "")
        {
            List<Khoahoc> kh = db.Khoahocs.Where(row => row.UserId == UserId).ToList();
            if (!String.IsNullOrEmpty(search))
            {
                kh = kh.Where(s => s.name.Contains(search) && s.UserId == UserId).ToList();
            }
            ViewBag.UserId = UserId;
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
            k.UserId = kh.UserId;
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
        [HttpGet]
        public ActionResult YourPost(string UserId,string search = "")
        {
            List<Post_Post> post_Posts = db.Post_Posts.Where(row => row.UserId == UserId).ToList();
            if (!String.IsNullOrEmpty(search))
            {
                post_Posts = post_Posts.Where(s => s.Title.Contains(search) && s.UserId == UserId).ToList();
            }
            ViewBag.UserId = UserId;
            return View(post_Posts);
        }
        [HttpGet]
        public ActionResult Create_Blog(string UserId)
        {
            Post_Post model = new Post_Post();
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser UserToPost = UserManager.FindById(UserId);
            if (UserToPost != null)
            {
                model.UserId = UserToPost.Id;
                model.DateCreated = DateTime.Now;
                model.Status = Post_Post.PostStatus.Pending;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create_Blog(Post_Post model)
        {
            if (ModelState.IsValid)
            {
                db.Post_Posts.Add(model);
                db.SaveChanges();
                return RedirectToAction("YourPost", new { UserId = User.Identity.GetUserId() });
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult RollOut(int? classID, string date)
        {
            /*var lophoc = db.Lophocs.FirstOrDefault(l => l.class_id == id);
            var lichhoc = db.Lichhocs.FirstOrDefault(l => l.lophoc_id == id);*/
            var lophoc = db.Lophocs.ToList();
            var members = new List<ClassMember>();
            var lichHocs = new Lichhoc();
            ViewBag.selectedDate = date;
            if (classID != null)
            {
                lichHocs = db.Lichhocs
               .Where(row => row.lophoc_id == classID)
               .OrderByDescending(row => row.id)
               .FirstOrDefault();
                date = lichHocs.Ngayhoc1;
            }
            if (date != null)
            {
                members = db.ClassMembers.Where(row => row.lophoc_id == classID).ToList();
            }
            ViewBag.currentClass = classID;
            var viewModel = new RollOutModel
            {
                Lophoc = lophoc,
                Lichhoc = lichHocs,
                ClassMembers = members,
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult RollOut(List<RollOut> rollouts)
        {
            try
            {
                // Thêm tất cả các bản ghi vào DbSet
                foreach (var item in rollouts)
                {
                    db.RollOuts.Add(item);
                }

                // Lưu thay đổi vào cơ sở dữ liệu một lần
                db.SaveChanges();

                // Trả về kết quả JSON thành công
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và hiển thị thông báo lỗi ra console
                System.Diagnostics.Debug.WriteLine("Error Add: " + ex.ToString());

                // Trả về thông báo lỗi trong trường hợp xảy ra lỗi
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult FetchData(int? classId, string selectedDate)
        {
            return RedirectToAction("RollOut", "Teacher", new { classID = classId, date = selectedDate });
        }

    }
}
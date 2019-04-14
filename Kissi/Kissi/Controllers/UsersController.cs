using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Kissi.Models;
using Kissi.Classes;

namespace Kissi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private KissiContext db = new KissiContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.City).Include(u => u.Company).Include(u => u.Department);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(0), "CityId", "Name");
            ViewBag.CompanyId = new SelectList(CombosHelper.GetCompanies(), "CompanyId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                UsersHelper.CreateUserASP(user.UserName, "User");
                if (user.PhotoFile != null)
                {
                    var folder = "~/Content/Users";
                    var file = string.Format("{0}.jpg", user.UserId);
                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response)
                    {

                        var pic = string.Format("{0}/{1}.jpg", folder, user.UserId);
                        user.Photo = pic;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(user.DepartmentId), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelper.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", user.DepartmentId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(user.DepartmentId), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelper.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", user.DepartmentId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( User user)
        {
            if (ModelState.IsValid)
            {
                if (user.PhotoFile != null)
                {
                    var folder = "~/Content/Users";
                    var pic = string.Empty;

                    var file = string.Format("{0}.jpg", user.UserId);
                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}.jpg", folder, file);
                        user.Photo = pic;
                    }
                }

                var db2 = new KissiContext();
                var currentUser = db2.Users.Find(user.UserId);
                if (currentUser.UserName !=user.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, user.UserName);
                }
                db2.Dispose();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(user.DepartmentId), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelper.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", user.DepartmentId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            UsersHelper.DeleteUser(user.UserName);
            return RedirectToAction("Index");
        }
        //public JsonResult Getcities(int departmentId)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var cities = db.Cities.Where(m => m.DepartmentId == departmentId);
        //    return Json(cities);
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

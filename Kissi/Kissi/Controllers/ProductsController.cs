using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Kissi.Models;
using Kissi.Classes;

namespace Kissi.Controllers
{
    public class ProductsController : Controller
    {
        private KissiContext db = new KissiContext();

        // GET: Products
        public ActionResult Index()
        {
            var user = db.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var products = db.Products.Include(p => p.Category).Where(p => p.CompanyId==user.CompanyId).Include(p => p.Tax);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var product = new Product { CompanyId = user.CompanyId, };
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            ViewBag.TaxId = new SelectList(db.Taxes, "TaxId", "Description");
            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                if (product.ImageFile != null)
                {
                    var folder = "~/Content/Products";
                    var file = string.Format("{0}.jpg", product.ProductId);
                    var response = FilesHelper.UploadPhoto(product.ImageFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}.jpg", folder, product.ProductId);
                        product.Image = pic;
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(db.Taxes, "TaxId", "Description", product.TaxId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.TaxId = new SelectList(db.Taxes, "TaxId", "Description", product.TaxId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    var folder = "~/Content/Products";
                    var pic = string.Empty;

                    var file = string.Format("{0}.jpg", product.ProductId);
                    var response = FilesHelper.UploadPhoto(product.ImageFile, folder, file);
                    if (response)
                    {

                        pic = string.Format("{0}/{1}.jpg", folder, file);
                        product.Image = pic;

                    }
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.TaxId = new SelectList(db.Taxes, "TaxId", "Description", product.TaxId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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

using Kissi.Models;
using System.Linq;
using System.Web.Mvc;

namespace Kissi.Controllers
{
    public class GenericController : Controller
    {
        private KissiContext db = new KissiContext();
        // GET: Generic
        public JsonResult GetCities(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(c => c.DepartmentId == departmentId);
            return Json(cities);
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
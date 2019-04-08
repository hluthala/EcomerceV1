using Kissi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kissi.Controllers
{
    public class HomeController : Controller
    {
        private KissiContext db = new KissiContext();

        public ActionResult Index()
        {
            var user = db.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
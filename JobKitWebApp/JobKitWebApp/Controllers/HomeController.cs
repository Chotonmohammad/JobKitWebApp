using JobKitWebApp.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobKitWebApp.Controllers
{
    public class HomeController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();
        public ActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Admin(string email, string password, int? accountTypeId)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || accountTypeId == null)
            {
                return Json(new { error = true, error_msg = "Required Field are missing." }, JsonRequestBehavior.AllowGet);
            }
            if (accountTypeId == -1)
            {
                var client = db.Admins.Where(t => t.AdminEmail == email).Where(t => t.AdminPassword == password).SingleOrDefault();
                if (client == null || string.IsNullOrEmpty(client.AdminEmail) || client.AdminId <= 0)
                {
                    return Json(new { error = true, error_msg = "Invalid credentials." }, JsonRequestBehavior.AllowGet);
                }
                Session["AdminEmail"] = client.AdminEmail;
                Session["AdminId"] = client.AdminId;
                return Json(new { error = false, url = "/Admin/AllFreelancer/Index" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true, error_msg = "Something went wrong." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
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
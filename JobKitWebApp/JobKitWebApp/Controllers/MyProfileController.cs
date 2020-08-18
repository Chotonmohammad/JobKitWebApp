using JobKitWebApp.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobKitWebApp.Controllers
{
    public class MyProfileController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();
        // GET: MyProfile
        public ActionResult Jobs()
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int freelancerId = Convert.ToInt32(Session["FreelancerId"]);
            ViewBag.RecentJobs = db.ApplyJobs.Where(aj => aj.FreelancerId == freelancerId).OrderByDescending(t => t.ApplyJobId)
                    .Include(aj => aj.Job).Include(aj => aj.Job.User).Include(f => f.Job.FreelancerCategory).Include(aj => aj.Job.City).Include(aj => aj.Job.JobType).ToList();
            return View(ViewBag.RecentJobs);
        }
        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult ContactInfo()
        {
            return View();
        }
    }
}
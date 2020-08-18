using JobKitWebApp.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobKitWebApp.Controllers
{
    public class FreelancersController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext()
;        // GET: Freelancers
        public ActionResult ProfileInfo()
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int id = Convert.ToInt32(Session["FreelancerId"]);
            try
            {
                var freelancerInfo = db.Freelancers.Where(f => f.FreelancerId == id).Include(f => f.City)
                    .Include(f => f.FreelancerCategory).Include(f => f.ApplyJobs)
                    //.Select(p => new
                    //{
                    //    p.FreelancerId,
                    //    p.FreelancerName,
                    //    ApplyJobs = p.ApplyJobs.Select(t => new { t.Job, t.CoverMessage }).ToList()
                    //})
                    .SingleOrDefault();
                if (freelancerInfo == null )
                {
                    ViewBag.ProfileNotFoundMsg = "Profile Not Found";
                    return View();
                }
                ViewBag.RecentJobs = db.ApplyJobs.Where(aj => aj.JobConfirmFlag == 1).Where(aj => aj.FreelancerId == id).OrderByDescending(t => t.ApplyJobId)
                    .Include(aj => aj.Job).Include(aj => aj.Job.User).Include(f => f.Job.FreelancerCategory).Include(aj => aj.Job.City).Include(aj => aj.Job.JobType).Include(t=>t.UserFeedbacks).ToList();

                return View(freelancerInfo);

            }
            catch (Exception)
            {
                return Redirect("~/FindWorks/Index");
            }
        }

        
    }
}
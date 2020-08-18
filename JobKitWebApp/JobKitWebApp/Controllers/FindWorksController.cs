using JobKitWebApp.Context;
using JobKitWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobKitWebApp.Controllers
{
    public class FindWorksController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();
        // GET: FindWorks

        public ActionResult Home()
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int f_id = Convert.ToInt32(Session["FreelancerId"]);

            var f_info = db.Freelancers.SingleOrDefault(f => f.FreelancerId == f_id);

            if (f_info == null)
            {
                return Redirect("~/Home/Index");

            }

            List<Job> jobs = db.Jobs.Where(t => t.FreelancerCategoryId == f_info.FreelancerCategoryId)
                .Where(j => j.CityId == f_info.CityId)
                .Where(j => j.ApplyJobs.All(t => t.JobConfirmFlag == 0))
                .OrderByDescending(j => j.JobId)
                .Include(j => j.JobType)
                .Include(j => j.FreelancerCategory)
                .Include(j => j.City)
                .Include(j => j.User)
                .Include(j => j.ApplyJobs)
                .ToList();
            if (jobs == null)
            {
                return Redirect("~/FindWorks/Index");
            }
            ViewBag.Jobs = jobs;
            return View();
        }
        public ActionResult Index()
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            ViewBag.FreelancerCategoryId = db.FreelancerCategories.ToList();
            ViewBag.JobTypeId = db.JobTypes.ToList();
            ViewBag.CityId = db.Cities.ToList();
            return View();
        }
        public ActionResult Jobs(int? FreelancerCategoryId, int? JobTypeId, int? CityId)
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            if (FreelancerCategoryId == null || JobTypeId==null || CityId==null)
            {
                return Redirect("~/FindWorks/Index");
            }



            List<Job> jobs = db.Jobs.Where(t => t.FreelancerCategoryId == FreelancerCategoryId || FreelancerCategoryId == -1)
                .Where(j => j.JobTypeId == JobTypeId || JobTypeId == -1)
                .Where(j => j.CityId == CityId || CityId == -1)
                .Where(j => j.ApplyJobs.All(t=>t.JobConfirmFlag==0))
                .OrderByDescending(j=>j.JobId)
                .Include(j => j.JobType)
                .Include(j => j.FreelancerCategory)
                .Include(j => j.City)
                .Include(j => j.User)
                .Include(j => j.ApplyJobs)
                .ToList();
            if (jobs == null)
            {
                return Redirect("~/FindWorks/Index");
            }
            ViewBag.Jobs = jobs;
            return View();
        }
        [HttpGet]
        public ActionResult ApplyJob(int? JobId)
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            if (JobId == null)
            {
                return Redirect("~/FindWorks/Index");
            }
            Job job = db.Jobs.Where(t => t.JobId == JobId)
                .OrderByDescending(j => j.JobId)
                .Include(j => j.JobType)
                .Include(j => j.FreelancerCategory)
                .Include(j => j.City)
                .Include(j => j.User).SingleOrDefault();

            if (job == null)
            {
                return Redirect("~/FindWorks/Index");
            }
            int f_id = Convert.ToInt32(Session["FreelancerId"]);
            ViewBag.Job = job;

            var f_info = db.Freelancers.SingleOrDefault(f => f.FreelancerId == f_id);
            ViewBag.MyInfo = f_info;
            return View();
        }
        [HttpPost]
        public ActionResult ApplyJob(ApplyJob applyJob)
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            applyJob.FreelancerId = Convert.ToInt32(Session["FreelancerId"]);
            if (ModelState.IsValid)
            {
                try
                {
                    db.ApplyJobs.Add(applyJob);

                    int rowAffected = db.SaveChanges();
                    if (rowAffected > 0)
                    {
                        var result = db.Freelancers.SingleOrDefault(b => b.FreelancerId == applyJob.FreelancerId);
                        result.TotalConnect += -1;
                        db.SaveChanges();
                        ViewBag.ApplyConfirmMsg = "Successfully done. Client will contact with you soon.";
                        return View();

                    }
                    ViewBag.ApplyConfirmMsg = "You may submit a proposal before.";
                    return View();
                }
                catch (Exception)
                {
                    ViewBag.ApplyConfirmMsg = "You may submit a proposal before.";
                    return View();
                }
            }

            return View();

        }
    }
}
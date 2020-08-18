using JobKitWebApp.Context;
using JobKitWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobKitWebApp.Controllers
{
    public class JobsController : Controller
    {
        private JobKitDbContext jobKitDbContext = new JobKitDbContext();
        // GET: Jobs
        public ActionResult WorkUpdate(int? JobId)
        {
            if (JobId == null)
            {
                return Redirect("~/MyProfile/Jobs");
            }
            var job_info = jobKitDbContext.Jobs.SingleOrDefault(b => b.JobId == JobId);
            if (job_info == null)
            {
                return Redirect("~/MyProfile/Jobs");
            }
            return View(job_info);
        }

        [HttpPost]
        public ActionResult WorkUpdate(Job job)
        {
            if (job.JobId <= 0)
            {
                return Redirect("~/MyProfile/Jobs");

            }
            var job_info = jobKitDbContext.Jobs.SingleOrDefault(b => b.JobId == job.JobId);

            job_info.WorkProgess = job.WorkProgess;
            jobKitDbContext.SaveChanges();

            return Redirect("~/MyProfile/Jobs");


        }
    }
}
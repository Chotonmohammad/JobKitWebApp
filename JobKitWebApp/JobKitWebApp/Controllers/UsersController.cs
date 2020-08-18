using JobKitWebApp.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JobKitWebApp.Models;

namespace JobKitWebApp.Controllers
{
    public class UsersController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Post_Job()
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            ViewBag.FreelancerCategoryId = new SelectList(db.FreelancerCategories, "FreelancerCategoryId", "FreelancerCategoryName");
            ViewBag.JobTypeId = new SelectList(db.JobTypes, "JobTypeId", "JobTypeName");
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
            return View();
        }
        [HttpPost]
        public ActionResult Post_Job(Job job)
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            job.UserId = Convert.ToInt32(Session["UserId"]);

            if (ModelState.IsValid)
            {
                try
                {
                    db.Jobs.Add(job);
                    int rowAffect = db.SaveChanges();
                    if (rowAffect > 0)
                    {
                        ViewBag.JobPostConfirmMsg = "Successfully done. Freelancers will contact with you soon.";
                        ViewBag.FreelancerCategoryId = new SelectList(db.FreelancerCategories, "FreelancerCategoryId", "FreelancerCategoryName");
                        ViewBag.JobTypeId = new SelectList(db.JobTypes,"JobTypeId","JobTypeName");
                        ViewBag.CityId = new SelectList(db.Cities,"CityId","CityName");
                        return View();
                    }
                    return Redirect("~/Users/Index");
                }
                catch (Exception)
                {
                    return Redirect("~/Users/Index");
                }
            }
            ViewBag.JobPostConfirmMsg = "Required field are missing.";
            ViewBag.FreelancerCategoryId = db.FreelancerCategories.ToList();
            ViewBag.JobTypeId = db.JobTypes.ToList();
            ViewBag.CityId = db.Cities.ToList();
            return View();
        }

        public ActionResult MyJobs()
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            var myjobs = db.Jobs.Where(aj => aj.UserId == userId)
                    .Include(aj => aj.ApplyJobs).Include(aj => aj.User).Include(f => f.FreelancerCategory).Include(aj => aj.City).Include(aj => aj.JobType).OrderByDescending(t=>t.JobId).ToList();

            if (myjobs == null)
            {
                return Redirect("~/Users/Index");
            }
            ViewBag.RecentJobs = myjobs;
            return View(ViewBag.RecentJobs);
        }

        public ActionResult View_Proposals(int? job_id)
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            var myjobInfo = db.Jobs.Where(aj => aj.UserId == userId).Where(t=>t.JobId==job_id).OrderByDescending(t => t.JobId)
                    .Include(aj => aj.ApplyJobs).Include(aj => aj.User).Include(f => f.FreelancerCategory).Include(aj => aj.City).Include(aj => aj.JobType).SingleOrDefault();

            if (myjobInfo == null)
            {
                return Redirect("~/Users/Index");
            }
            ViewBag.JobInfo = myjobInfo;
            ViewBag.JobProposals = db.ApplyJobs.Where(t => t.JobId == job_id).Include(t=>t.Freelancer).Include(f => f.Job).Include(f => f.Job.JobType).Include(t=>t.UserFeedbacks).ToList();
            return View(ViewBag.JobInfo);
        }

        public ActionResult ProfileInfo()
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int id = Convert.ToInt32(Session["UserId"]);
            
            var user_info = db.Users.Include(t=>t.Jobs).Include(t => t.Jobs).SingleOrDefault(t => t.UserId == id);
            if (user_info == null)
            {
                ViewBag.ProfileNotFoundMsg = "Profile Not Found";
                return View();
            }
            ViewBag.RecentJobs = db.Jobs.Where(aj => aj.UserId == id).OrderByDescending(t => t.JobId)
                    .Include(aj => aj.ApplyJobs).Include(aj => aj.User).Include(f => f.FreelancerCategory).Include(aj => aj.City).Include(aj => aj.JobType).ToList();

            return View(user_info);
        }
        public ActionResult ViewMode(int? freelancer_id)
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            if (freelancer_id == null)
            {
                return Redirect("~/Users/Index");
            }
            try
            {
                var freelancerInfo = db.Freelancers.Where(f => f.FreelancerId == freelancer_id).Include(f => f.City)
                    .Include(f => f.FreelancerCategory).Include(f => f.ApplyJobs)
                    //.Select(p => new
                    //{
                    //    p.FreelancerId,
                    //    p.FreelancerName,
                    //    ApplyJobs = p.ApplyJobs.Select(t => new { t.Job, t.CoverMessage }).ToList()
                    //})
                    .SingleOrDefault();
                if (freelancerInfo == null)
                {
                    ViewBag.ProfileNotFoundMsg = "Profile Not Found";
                    return View();
                }
                ViewBag.RecentJobs = db.ApplyJobs.Where(aj => aj.JobConfirmFlag == 1).Where(aj => aj.FreelancerId == freelancer_id).OrderByDescending(t => t.ApplyJobId)
                    .Include(aj => aj.Job).Include(aj => aj.Job.User).Include(f => f.Job.FreelancerCategory).Include(aj => aj.Job.City).Include(aj => aj.Job.JobType).ToList();

                return View(freelancerInfo);

            }
            catch (Exception)
            {
                return Redirect("~/Users/Index");
            }
        }

        public ActionResult Confirm_Freelancer(int? apply_job_id)
        {
            if (apply_job_id == null)
            {
                return Redirect("~/Users/MyJobs");
            }

            var apply_job = db.ApplyJobs.SingleOrDefault(s => s.ApplyJobId == apply_job_id);

            var job_info = db.Jobs.SingleOrDefault(j => j.JobId == apply_job.JobId);
            if (job_info == null)
            {
                return Redirect("~/Users/MyJobs");
            }
            if (apply_job == null)
            {
                return Redirect("~/Users/MyJobs");

            }

            job_info.JobActiveFlag = 1;
            db.SaveChanges();
            
            apply_job.JobConfirmFlag = 1;
            db.SaveChanges();

            return Redirect("~/Users/View_Proposals?job_id=" + apply_job.JobId);

        }

        public ActionResult Inbox(int? apply_job_id)
        {
            if (Session["UserId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            
            ViewBag.ApplyJobInfo = db.ApplyJobs.Where(t => t.ApplyJobId == apply_job_id).Include(t => t.Freelancer)
                .Include(t => t.Freelancer.City).Include(t => t.Freelancer.FreelancerCategory)
                .Include(t => t.UserFeedbacks).Include(t => t.FreelancerFeedbacks)
                .Include(f => f.Job).Include(f => f.Job.JobType).Include(f => f.Job.FreelancerCategory)
                .Include(f => f.Job.City).Include(f => f.Job.User).ToList();

            return View(ViewBag.JobInfo);
        }
        public ActionResult PaymentProcedure()
        {
            return View();
        }

        public ActionResult UserFeedback(int? jobApplyId)
        {
            if (Session["UserId"] == null || jobApplyId == null)
            {
                return Redirect("~/Users/Index");
            }

            var jobApplyInfoById = db.ApplyJobs.SingleOrDefault(b => b.ApplyJobId == jobApplyId);
            if (jobApplyInfoById == null)
            {
                return Redirect("~/Users/Index");

            }

            int UserId = Convert.ToInt32(Session["UserId"]);

            var my_apply_job_info = db.ApplyJobs.Where(aj => aj.Job.UserId == UserId).Where(t => t.JobId == jobApplyInfoById.JobId)
                    .Where(t => t.JobConfirmFlag == 1).OrderByDescending(t => t.JobId)
                    .Include(aj => aj.Job).Include(aj => aj.Job.User).Include(f => f.Job.FreelancerCategory).Include(aj => aj.Job.City).Include(aj => aj.Job.JobType)
                    .Include(aj => aj.FreelancerFeedbacks).Include(aj => aj.UserFeedbacks).Include(aj => aj.ApplyJobConversations).SingleOrDefault();

            if (my_apply_job_info == null)
            {
                return Redirect("~/Home/Index");
            }

            ViewBag.JobInfo = my_apply_job_info;
            return View(my_apply_job_info);
        }
        [HttpPost]
        public ActionResult UserFeedback([Bind(Include = "UserReply,JobApplyId")]UserFeedback userFeedback)
        {

            if (ModelState.IsValid)
            {
                db.UserFeedbacks.Add(userFeedback);
                int rowAffect = db.SaveChanges();
                if (rowAffect > 0)
                {
                    return Json(new { error = false, url = "/Users/UserFeedback?jobApplyId=" + userFeedback.JobApplyId }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { error = true, message = "Something went worng. PLease try again." }, JsonRequestBehavior.AllowGet);



            }
            return Json(new { error = true, message = "Something went worng. PLease try again." }, JsonRequestBehavior.AllowGet);

        }

    }
}
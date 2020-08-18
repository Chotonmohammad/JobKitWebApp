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
    public class InboxController : Controller
    {
        // GET: Inbox
        private JobKitDbContext db = new JobKitDbContext();

        // GET: Inbox
        public ActionResult UserReply(int? jobApplyId)
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
                    .Where(t=>t.JobConfirmFlag==1).OrderByDescending(t => t.JobId)
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

        public ActionResult UserReply([Bind(Include = "Reply,JobApplyId")]ApplyJobConversation applyJobConversation)
        {
            applyJobConversation.ConversationTypeFlag = 2;

            if (ModelState.IsValid)
            {
                db.ApplyJobConversations.Add(applyJobConversation);
                int rowAffect = db.SaveChanges();
                if (rowAffect > 0)
                {
                    return Json(new { error = false, url = "/Inbox/UserReply?jobApplyId=" + applyJobConversation.JobApplyId }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { error = true, message = "Something went worng. PLease try again." }, JsonRequestBehavior.AllowGet);



            }
            return Json(new { error = true, message = "Something went worng. PLease try again." }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FreelancerReply(int? jobApplyId)
        {
            if (Session["FreelancerId"] == null || jobApplyId == null)
            {
                return Redirect("~/Home/Index");
            }

            var jobApplyInfoById = db.ApplyJobs.SingleOrDefault(b => b.ApplyJobId == jobApplyId);
            if (jobApplyInfoById == null)
            {
                return Redirect("~/Home/Index");

            }

            int FreelancerId = Convert.ToInt32(Session["FreelancerId"]);

            var my_apply_job_info = db.ApplyJobs.Where(aj => aj.FreelancerId == FreelancerId).Where(t => t.JobId == jobApplyInfoById.JobId).Where(t => t.JobConfirmFlag == 1).OrderByDescending(t => t.JobId)
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

        public ActionResult FreelancerReply([Bind(Include = "Reply,JobApplyId")]ApplyJobConversation applyJobConversation)
        {
            applyJobConversation.ConversationTypeFlag = 1;

            if (ModelState.IsValid)
            {
                db.ApplyJobConversations.Add(applyJobConversation);
                int rowAffect = db.SaveChanges();
                if (rowAffect > 0)
                {
                    return Json(new { error = false, url = "/Inbox/FreelancerReply?jobApplyId="+applyJobConversation.JobApplyId }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { error = true, message = "Something went worng. PLease try again." }, JsonRequestBehavior.AllowGet);



            }
            return Json(new { error = true, message = "Something went worng. PLease try again." }, JsonRequestBehavior.AllowGet);

        }
    }
}
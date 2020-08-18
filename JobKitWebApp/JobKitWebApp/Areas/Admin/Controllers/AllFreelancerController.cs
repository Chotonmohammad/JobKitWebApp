using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobKitWebApp.Context;
using JobKitWebApp.Models;

namespace JobKitWebApp.Areas.Admin.Controllers
{
    public class AllFreelancerController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();

        public ActionResult ViewAll()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            var freelancers = db.Freelancers.Include(f => f.City).Include(f => f.FreelancerCategory).OrderByDescending(f => f.FreelancerId);
            return View(freelancers.ToList());
        }
        // GET: Admin/AllFreelancer
        public ActionResult Index()
        {
            if(Session["AdminEmail"] ==null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            var freelancers = db.Freelancers.Include(f => f.City).Include(f => f.FreelancerCategory).OrderByDescending(f=>f.FreelancerId);
            return View(freelancers.OrderByDescending(p=>p.FreelancerId).ToList());
        }

        // GET: Admin/AllFreelancer/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // GET: Admin/AllFreelancer/Create
        public ActionResult Create()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
            ViewBag.FreelancerCategoryId = new SelectList(db.FreelancerCategories, "FreelancerCategoryId", "FreelancerCategoryName");
            return View();
        }

        // POST: Admin/AllFreelancer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FreelancerId,FreelancerName,FreelancerTitle,FreelancerIntroduction,FreelancerCategoryId,CityId,TotalConnect,Address,Phone,Email,EmailVerified,VerifiedFlag,Password,PhotoUrl")] Freelancer freelancer)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                db.Freelancers.Add(freelancer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", freelancer.CityId);
            ViewBag.FreelancerCategoryId = new SelectList(db.FreelancerCategories, "FreelancerCategoryId", "FreelancerCategoryName", freelancer.FreelancerCategoryId);
            return View(freelancer);
        }

        // GET: Admin/AllFreelancer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", freelancer.CityId);
            ViewBag.FreelancerCategoryId = new SelectList(db.FreelancerCategories, "FreelancerCategoryId", "FreelancerCategoryName", freelancer.FreelancerCategoryId);
            return View(freelancer);
        }

        // POST: Admin/AllFreelancer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FreelancerId,VerifiedFlag")] Freelancer freelancer)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (freelancer.FreelancerId<0)
            {
                
                return RedirectToAction("Index");
            }
            try
            {
                var result = db.Freelancers.SingleOrDefault(t => t.FreelancerId == freelancer.FreelancerId);
                result.VerifiedFlag = freelancer.VerifiedFlag;
                int rowaffect = db.SaveChanges();
                if (rowaffect > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");

            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", freelancer.CityId);
            ViewBag.FreelancerCategoryId = new SelectList(db.FreelancerCategories, "FreelancerCategoryId", "FreelancerCategoryName", freelancer.FreelancerCategoryId);
            return View(freelancer);
        }

        // GET: Admin/AllFreelancer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // POST: Admin/AllFreelancer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            db.Freelancers.Remove(freelancer);
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

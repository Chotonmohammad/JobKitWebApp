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
    public class JobTypesController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();

        // GET: Admin/JobTypes
        public ActionResult Index()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            return View(db.JobTypes.ToList());
        }

        // GET: Admin/JobTypes/Details/5
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
            JobType jobType = db.JobTypes.Find(id);
            if (jobType == null)
            {
                return HttpNotFound();
            }
            return View(jobType);
        }

        // GET: Admin/JobTypes/Create
        public ActionResult Create()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            return View();
        }

        // POST: Admin/JobTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobTypeId,JobTypeName")] JobType jobType)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                db.JobTypes.Add(jobType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobType);
        }

        // GET: Admin/JobTypes/Edit/5
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
            JobType jobType = db.JobTypes.Find(id);
            if (jobType == null)
            {
                return HttpNotFound();
            }
            return View(jobType);
        }

        // POST: Admin/JobTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobTypeId,JobTypeName")] JobType jobType)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(jobType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobType);
        }

        // GET: Admin/JobTypes/Delete/5
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
            JobType jobType = db.JobTypes.Find(id);
            if (jobType == null)
            {
                return HttpNotFound();
            }
            return View(jobType);
        }

        // POST: Admin/JobTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            JobType jobType = db.JobTypes.Find(id);
            db.JobTypes.Remove(jobType);
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

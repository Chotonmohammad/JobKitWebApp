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
    public class FreelancerCategoriesController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();

        // GET: Admin/FreelancerCategories
        public ActionResult Index()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            return View(db.FreelancerCategories.ToList());
        }

        // GET: Admin/FreelancerCategories/Details/5
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
            FreelancerCategory freelancerCategory = db.FreelancerCategories.Find(id);
            if (freelancerCategory == null)
            {
                return HttpNotFound();
            }
            return View(freelancerCategory);
        }

        // GET: Admin/FreelancerCategories/Create
        public ActionResult Create()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            return View();
        }

        // POST: Admin/FreelancerCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FreelancerCategoryId,FreelancerCategoryName")] FreelancerCategory freelancerCategory)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                db.FreelancerCategories.Add(freelancerCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freelancerCategory);
        }

        // GET: Admin/FreelancerCategories/Edit/5
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
            FreelancerCategory freelancerCategory = db.FreelancerCategories.Find(id);
            if (freelancerCategory == null)
            {
                return HttpNotFound();
            }
            return View(freelancerCategory);
        }

        // POST: Admin/FreelancerCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FreelancerCategoryId,FreelancerCategoryName")] FreelancerCategory freelancerCategory)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(freelancerCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(freelancerCategory);
        }

        // GET: Admin/FreelancerCategories/Delete/5
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
            FreelancerCategory freelancerCategory = db.FreelancerCategories.Find(id);
            if (freelancerCategory == null)
            {
                return HttpNotFound();
            }
            return View(freelancerCategory);
        }

        // POST: Admin/FreelancerCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            FreelancerCategory freelancerCategory = db.FreelancerCategories.Find(id);
            db.FreelancerCategories.Remove(freelancerCategory);
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

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
    public class ConnectPricingsController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();

        // GET: Admin/ConnectPricings
        public ActionResult Index()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            return View(db.ConnectPricings.ToList());
        }

        // GET: Admin/ConnectPricings/Details/5
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
            ConnectPricing connectPricing = db.ConnectPricings.Find(id);
            if (connectPricing == null)
            {
                return HttpNotFound();
            }
            return View(connectPricing);
        }

        // GET: Admin/ConnectPricings/Create
        public ActionResult Create()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            return View();
        }

        // POST: Admin/ConnectPricings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectPricingId,NumberOfConnect,Price")] ConnectPricing connectPricing)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                db.ConnectPricings.Add(connectPricing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(connectPricing);
        }

        // GET: Admin/ConnectPricings/Edit/5
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
            ConnectPricing connectPricing = db.ConnectPricings.Find(id);
            if (connectPricing == null)
            {
                return HttpNotFound();
            }
            return View(connectPricing);
        }

        // POST: Admin/ConnectPricings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConnectPricingId,NumberOfConnect,Price")] ConnectPricing connectPricing)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(connectPricing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(connectPricing);
        }

        // GET: Admin/ConnectPricings/Delete/5
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
            ConnectPricing connectPricing = db.ConnectPricings.Find(id);
            if (connectPricing == null)
            {
                return HttpNotFound();
            }
            return View(connectPricing);
        }

        // POST: Admin/ConnectPricings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            ConnectPricing connectPricing = db.ConnectPricings.Find(id);
            db.ConnectPricings.Remove(connectPricing);
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

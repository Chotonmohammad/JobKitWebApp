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
    public class ConnectsController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();

        // GET: Admin/Connects
        public ActionResult Index()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            var connects = db.Connects.Include(c => c.ConnectPricing).Include(c => c.Freelancer).OrderByDescending(c=>c.ConnectId);
            return View(connects.ToList());
        }

        // GET: Admin/Connects/Details/5
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
            Connect connect = db.Connects.Find(id);
            if (connect == null)
            {
                return HttpNotFound();
            }
            return View(connect);
        }

        // GET: Admin/Connects/Create
        public ActionResult Create()
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            ViewBag.ConnectPriceId = new SelectList(db.ConnectPricings, "ConnectPricingId", "ConnectPricingId");
            ViewBag.FreelancerId = new SelectList(db.Freelancers, "FreelancerId", "FreelancerName");
            return View();
        }

        // POST: Admin/Connects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectId,ConnectPriceId,FreelancerId,Refernce,PaymentStatus")] Connect connect)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                db.Connects.Add(connect);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConnectPriceId = new SelectList(db.ConnectPricings, "ConnectPricingId", "ConnectPricingId", connect.ConnectPriceId);
            ViewBag.FreelancerId = new SelectList(db.Freelancers, "FreelancerId", "FreelancerName", connect.FreelancerId);
            return View(connect);
        }

        // GET: Admin/Connects/Edit/5
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
            Connect connect = db.Connects.Find(id);
            if (connect == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConnectPriceId = new SelectList(db.ConnectPricings, "ConnectPricingId", "ConnectPricingId", connect.ConnectPriceId);
            ViewBag.FreelancerId = new SelectList(db.Freelancers, "FreelancerId", "FreelancerName", connect.FreelancerId);
            return View(connect);
        }

        // POST: Admin/Connects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Connect connect)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            if (ModelState.IsValid)
            {
                var result = db.Connects.SingleOrDefault(c => c.ConnectId == connect.ConnectId);
                if (result.PaymentStatus == -1)
                {
                    result.PaymentStatus = connect.PaymentStatus;
                    db.SaveChanges();

                    var f_info = db.Freelancers.SingleOrDefault(f => f.FreelancerId == connect.FreelancerId);

                    var c_p_info = db.ConnectPricings.SingleOrDefault(f => f.ConnectPricingId == connect.ConnectPriceId);

                    f_info.TotalConnect += c_p_info.NumberOfConnect;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            ViewBag.ConnectPriceId = new SelectList(db.ConnectPricings, "ConnectPricingId", "ConnectPricingId", connect.ConnectPriceId);
            ViewBag.FreelancerId = new SelectList(db.Freelancers, "FreelancerId", "FreelancerName", connect.FreelancerId);
            return View(connect);
        }

        // GET: Admin/Connects/Delete/5
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
            Connect connect = db.Connects.Find(id);
            if (connect == null)
            {
                return HttpNotFound();
            }
            return View(connect);
        }

        // POST: Admin/Connects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminEmail"] == null || Session["AdminId"] == null)
            {
                return Redirect("~/Home/Admin");
            }
            Connect connect = db.Connects.Find(id);
            db.Connects.Remove(connect);
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

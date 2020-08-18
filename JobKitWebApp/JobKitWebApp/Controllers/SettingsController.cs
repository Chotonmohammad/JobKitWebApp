using JobKitWebApp.Context;
using JobKitWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JobKitWebApp.Controllers
{
    public class SettingsController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();

        // GET: Settings
        public ActionResult ProfileIntroduction()
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int f_id = Convert.ToInt32(Session["FreelancerId"]);
            var f_info = db.Freelancers.Where(f => f.FreelancerId == f_id).Include(f=>f.Connects)
                .SingleOrDefault();
            return View(f_info);
        }
        [HttpPost]
        public ActionResult ProfileIntroduction(Freelancer freelancer)
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            try
            {
                int f_id = Convert.ToInt32(Session["FreelancerId"]);
                var f_info = db.Freelancers.Where(f => f.FreelancerId == f_id)
                    .SingleOrDefault();
                if (f_info == null)
                {
                    return Redirect("~/Home/Index");

                }
                f_info.FreelancerTitle = freelancer.FreelancerTitle;
                f_info.FreelancerName = freelancer.FreelancerName;
                f_info.FreelancerIntroduction = freelancer.FreelancerIntroduction;
                db.SaveChanges();
                return Redirect("~/Settings/ProfileIntroduction");
            }
            catch (Exception)
            {
                return Redirect("~/Settings/ProfileIntroduction");
            }
        }

        public ActionResult ContactInfo()
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int f_id = Convert.ToInt32(Session["FreelancerId"]);
            var f_info = db.Freelancers.Where(f => f.FreelancerId == f_id).Include(f => f.Connects)
                .SingleOrDefault();
            return View(f_info);
        }
        [HttpPost]
        public ActionResult ContactInfo(Freelancer freelancer)
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            try
            {
                int f_id = Convert.ToInt32(Session["FreelancerId"]);
                var f_info = db.Freelancers.Where(f => f.FreelancerId == f_id)
                    .SingleOrDefault();
                if (f_info == null)
                {
                    return Redirect("~/Home/Index");

                }
                f_info.Address = freelancer.Address;
                f_info.Phone = freelancer.Phone;
                db.SaveChanges();
                return Redirect("~/Settings/ContactInfo");
            }
            catch (Exception)
            {
                return Redirect("~/Settings/ContactInfo");
            }
        }

        public ActionResult Connects()
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            int f_id = Convert.ToInt32(Session["FreelancerId"]);
            var f_info = db.Freelancers.Where(f => f.FreelancerId == f_id)
                .SingleOrDefault();
            var connect_info = db.Connects.Where(c => c.FreelancerId == f_id).Include(f => f.ConnectPricing).OrderByDescending(c=>c.ConnectId).ToList();
            ViewBag.Connects = connect_info;
            return View(f_info);

        }

        public List<SelectListItem> GetAllConnectPricingForDropdown()
        {
            List<ConnectPricing> connectpricingList = db.ConnectPricings.ToList();
            List<SelectListItem> selectListItemList = new List<SelectListItem>
            {
                new SelectListItem(){ Text = "--Select Connect Pricing--", Value = ""}
            };
            foreach (ConnectPricing aPriceUnit in connectpricingList)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = aPriceUnit.NumberOfConnect+" Connects - BDT "+aPriceUnit.Price;
                selectListItem.Value = aPriceUnit.ConnectPricingId.ToString();
                selectListItemList.Add(selectListItem);
            }
            return selectListItemList;
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public ActionResult Add_Connects()
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            ViewBag.ConnectPriceId = GetAllConnectPricingForDropdown();
            return View();
        }
        [HttpPost]

        public ActionResult Add_Connects(Connect connect)
        {
            if (Session["FreelancerId"] == null)
            {
                return Redirect("~/Home/Index");
            }
            if (connect.ConnectPriceId <= 0)
            {
                return Redirect("~/Home/Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int f_id = Convert.ToInt32(Session["FreelancerId"]);
                    connect.FreelancerId = f_id;
                    connect.Refernce = f_id + RandomString(5, false);
                    db.Connects.Add(connect);
                    db.SaveChanges();
                    return Redirect("~/Settings/Connects");

                }
                catch (Exception)
                {
                    return Redirect("~/Settings/Connects");

                }
            }

            ViewBag.ConnectPricingId = GetAllConnectPricingForDropdown();
            return Redirect("~/Settings/Connects");
        }

        public ActionResult Payment_Procedure(int? connect_id)
        {
            if (connect_id == null)
            {
                return Redirect("~/Home/Index");
            }
            int f_id = Convert.ToInt32(Session["FreelancerId"]);

            var payment_procedure = db.Connects.Where(c => c.ConnectId == connect_id).Where(c => c.FreelancerId == f_id).Include(c => c.ConnectPricing).SingleOrDefault();

            if (payment_procedure == null)
            {
                return Redirect("~/Settings/Connects");

            }
            return View(payment_procedure);
        }
    }
}
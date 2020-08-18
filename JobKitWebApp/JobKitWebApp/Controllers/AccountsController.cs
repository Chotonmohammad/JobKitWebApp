using JobKitWebApp.Context;
using JobKitWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JobKitWebApp.Controllers
{
    public class AccountsController : Controller
    {
        private JobKitDbContext db = new JobKitDbContext();
        // GET: Users
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string email,string password,int? accountTypeId)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || accountTypeId==null)
            {
                return Json(new { error=true,error_msg="Required Field are missing."},JsonRequestBehavior.AllowGet);
            }
            if (accountTypeId == 1)
            {
                var client = db.Users.Where(t => t.Email == email).Where(t => t.Password == password).SingleOrDefault();
                if (client == null || string.IsNullOrEmpty(client.Email) || client.UserId <= 0)
                {
                    return Json(new { error = true, error_msg = "Invalid credentials." }, JsonRequestBehavior.AllowGet);
                }
                Session["UserEmail"] = client.Email;
                Session["UserId"] = client.UserId;
                if (client.EmailVerifiedFlag == -1)
                {
                    Session["USignUpCheck"] = 1;
                    return Json(new { error = false, url = "/Accounts/U_Verify" }, JsonRequestBehavior.AllowGet);

                }


                return Json(new { error = false, url = "/Users/Index" }, JsonRequestBehavior.AllowGet);
            }
            else if (accountTypeId == 2)
            {
                var freelancer = db.Freelancers.Where(t => t.Email == email).Where(t => t.Password == password).SingleOrDefault();
                if (freelancer == null || string.IsNullOrEmpty(freelancer.Email) || freelancer.FreelancerId <= 0)
                {
                    return Json(new { error = true, error_msg = "Invalid credentials." }, JsonRequestBehavior.AllowGet);
                }
                Session["FreelancerEmail"] = freelancer.Email;
                Session["FreelancerId"] = freelancer.FreelancerId;
                if (freelancer.EmailVerified == -1)
                {
                    Session["FSignUpCheck"] = 1;
                return Json(new { error = false, url = "/Accounts/F_Verify" }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { error = false, url = "/FindWorks/Home" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true, error_msg = "Something went wrong." }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult SignInFreelancer(string email, string password)
        //{
        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        //    {
        //        return Json(new { error = true, error_msg = "Required Field are missing." }, JsonRequestBehavior.AllowGet);
        //    }
        //    var client = db.Freelancers.Where(t => t.Email == email).Where(t => t.Password == password).SingleOrDefault();
        //    if (client == null || string.IsNullOrEmpty(client.Email) || client.FreelancerId <= 0)
        //    {
        //        return Json(new { error = true, error_msg = "Invalid credentials." }, JsonRequestBehavior.AllowGet);
        //    }
        //    Session["FreelancerEmail"] = client.Email;
        //    Session["FreelancerId"] = client.FreelancerId;
        //    return Json(new { error = false, url = "~/FindWorks/Index" }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpGet]
        public ActionResult F_SignUp()
        {
            SelectList selectListItems = new SelectList(db.FreelancerCategories, "FreelancerCategoryId", "FreelancerCategoryName");
            SelectList cities = new SelectList(db.Cities, "CityId", "CityName");
            ViewBag.FreelancerCategoryId = selectListItems;
            ViewBag.CityId = cities;
            return View();
        }
        [HttpPost]
        public ActionResult F_SignUp(Freelancer freelancer)
        {
            if(string.IsNullOrEmpty(freelancer.Email) || freelancer.FreelancerCategoryId<=0 || string.IsNullOrEmpty(freelancer.FreelancerName) || freelancer.VerifiedFlag != -1 || string.IsNullOrEmpty(freelancer.Password))
            {
                ViewBag.SignUpMsg = "Required field are missing.";
                SelectList selectListItems = new SelectList(db.FreelancerCategories, "FreelancerCategoryId", "FreelancerCategoryName");
                ViewBag.FreelancerCategoryId = selectListItems;
                return View();
            }
            try
            {
                freelancer.TotalConnect = 20;
                freelancer.FreelancerTitle = "";
                freelancer.FreelancerIntroduction = "";
                freelancer.Address = "";
                freelancer.Phone = "";
                freelancer.PhotoUrl = "";
                db.Freelancers.Add(freelancer);
                int rowAffected = db.SaveChanges();
                if (rowAffected > 0)
                {
                    Session["FreelancerEmail"] = freelancer.Email;
                    Session["FreelancerId"] = freelancer.FreelancerId;
                    Session["FSignUpCheck"] = 1;
                    if (Session["FreelancerId"] != null)
                    {
                        return Redirect("~/Accounts/F_Verify");
                    }
                }
            }
            catch (Exception ex)
            {
                return Redirect("~/Accounts/SignIn");

            }
            return Redirect("~/Accounts/SignIn");


        }
        public string GenerateRandomNumber()
        {
            Random generator = new Random();
            int r = generator.Next(1, 1000000);
            string aRandomNumber = r.ToString().PadLeft(6, '0');
            return aRandomNumber;
        }

        public ActionResult F_Verify()
        {
            if (Session["FSignUpCheck"] == null)
            {
                return Redirect("~/Accounts/SignIn");

            }
            if (Session["FreelancerEmail"] == null)
            {
                return Redirect("~/Accounts/SignIn");

            }
            string email = Session["FreelancerEmail"].ToString();

            try
            {
                string randomNumber = GenerateRandomNumber();

                Session["RandomNumber"] = randomNumber;

                MailMessage mm = new MailMessage("\"JobKit\"<TeamJobKit@gmail.com>", email);
                mm.Subject = " Welcome To JobKit";


                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("TeamJobKit@gmail.com", "@JobKit+12#");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;



                //var inlineLogo1 = new LinkedResource(Server.MapPath("~/Content/mail_header_footer/header.png"), "image/png");
                var inlineLogo2 = new LinkedResource(Server.MapPath("~/Content/mail_header_footer/footer.png"), "image/png");
                //inlineLogo1.ContentId = Guid.NewGuid().ToString();
                inlineLogo2.ContentId = Guid.NewGuid().ToString();


                string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";

            //    body += string.Format(@"
            //<center>
            //<img src=""cid:{0}"" />
            //</center>           
            //<hr>
            
            //", inlineLogo1.ContentId);
                body += "</HEAD><BODY><DIV><FONT face=Arial color=#000000 size=3>Hi" + ",";
                body += "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>Welcome To jobKit";
                body +=
                    "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>JobKit received a request to use this email address to open JobKit Account." +
                    "We want to make sure its really you.In order to further verify your identity.";
                body +=
                    "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>Enter the following code to verify your email :";
                body += "</HEAD><BODY><br><DIV><FONT face=Arial color=#06F5C8 size=4>" + randomNumber;
                body += "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>Thanks for using JobKit";
                body += "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>Regards,";
                body += "</HEAD><BODY><DIV><FONT face=Arial color=#000000 size=3>Team Job Kit";

                body += string.Format(@"
            <hr>            
            <img src=""cid:{0}"" />           
            ", inlineLogo2.ContentId);
                body += "</FONT></DIV></BODY></HTML>";


                var view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                //view.LinkedResources.Add(inlineLogo1);
                view.LinkedResources.Add(inlineLogo2);

                mm.AlternateViews.Add(view);


                smtp.Send(mm);
            }
            catch (Exception)
            {
                return Redirect("~/Accounts/SignIn");
            }
            ViewBag.HomeMsg = "We want to make sure its really you.In order to further verify your identity,enter the verification code that was send to "+ email;
            return View();

        }
        [HttpPost]
        public ActionResult F_Verify(Freelancer freelancer)
        {
            if (Session["FreelancerEmail"] == null || Session["RandomNumber"]==null)
            {
                return Redirect("~/Accounts/SignIn");
            }
            if (Convert.ToInt32(Session["RandomNumber"]) != freelancer.VerificationCode)
            {
                ViewBag.VerificationMsg = "You entered invalid verification code, please try again.";
                return View();
            }
            //string email = Session["FreelancerEmail"].ToString();
            int freelancerId = Convert.ToInt32(Session["FreelancerId"]);
            try
            {
                var freelancer_info = db.Freelancers.SingleOrDefault(f => f.FreelancerId == freelancerId);
                if (freelancer_info == null)
                {
                    return Redirect("~/Accounts/SignIn");
                }
                freelancer_info.EmailVerified = 1;
                db.SaveChanges();
                return Redirect("~/Freelancers/ProfileInfo");
            }
            catch (Exception)
            {
                return Redirect("~/Accounts/SignIn");
            }
        }

        //Users Section

        [HttpGet]
        public ActionResult U_SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult U_SignUp(User user)
        {
            if (string.IsNullOrEmpty(user.Email)|| string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Name) || user.EmailVerifiedFlag != -1 || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.SignUpMsg = "Required field are missing.";
                return View();
            }
            try
            {
                db.Users.Add(user);
                int rowAffected = db.SaveChanges();
                if (rowAffected > 0)
                {
                    Session["UserEmail"] = user.Email;
                    Session["UserId"] = user.UserId;
                    Session["USignUpCheck"] = 1;
                    if (Session["UserId"] != null)
                    {
                        return Redirect("~/Accounts/U_Verify");
                    }
                }
            }
            catch (Exception ex)
            {
                return Redirect("~/Accounts/SignIn");

            }
            return Redirect("~/Accounts/SignIn");


        }

        public ActionResult U_Verify()
        {
            if (Session["USignUpCheck"] == null)
            {
                return Redirect("~/Accounts/SignIn");
            }
            if (Session["UserEmail"] == null)
            {
                return Redirect("~/Accounts/SignIn");

            }
            string email = Session["UserEmail"].ToString();

            try
            {
                string randomNumber = GenerateRandomNumber();

                Session["RandomNumber"] = randomNumber;

                MailMessage mm = new MailMessage("\"JobKit\"<TeamJobKit@gmail.com>", email);
                mm.Subject = " Welcome To JobKit";


                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("TeamJobKit@gmail.com", "@JobKit+12#");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;



                //var inlineLogo1 = new LinkedResource(Server.MapPath("~/Content/mail_header_footer/header.png"), "image/png");
                var inlineLogo2 = new LinkedResource(Server.MapPath("~/Content/mail_header_footer/footer.png"), "image/png");
                //inlineLogo1.ContentId = Guid.NewGuid().ToString();
                inlineLogo2.ContentId = Guid.NewGuid().ToString();


                string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";

                //    body += string.Format(@"
                //<center>
                //<img src=""cid:{0}"" />
                //</center>           
                //<hr>

                //", inlineLogo1.ContentId);
                body += "</HEAD><BODY><DIV><FONT face=Arial color=#000000 size=3>Hi" + ",";
                body += "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>Welcome To jobKit";
                body +=
                    "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>JobKit received a request to use this email address to open JobKit Account." +
                    "We want to make sure its really you.In order to further verify your identity.";
                body +=
                    "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>Enter the following code to verify your email :";
                body += "</HEAD><BODY><br><DIV><FONT face=Arial color=#06F5C8 size=4>" + randomNumber;
                body += "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>Thanks for using JobKit";
                body += "</HEAD><BODY><br><DIV><FONT face=Arial color=#000000 size=3>Regards,";
                body += "</HEAD><BODY><DIV><FONT face=Arial color=#000000 size=3>Team Job Kit";

                body += string.Format(@"
            <hr>            
            <img src=""cid:{0}"" />           
            ", inlineLogo2.ContentId);
                body += "</FONT></DIV></BODY></HTML>";


                var view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                //view.LinkedResources.Add(inlineLogo1);
                view.LinkedResources.Add(inlineLogo2);

                mm.AlternateViews.Add(view);


                smtp.Send(mm);
            }
            catch (Exception)
            {
                return Redirect("~/Accounts/SignIn");
            }
            ViewBag.HomeMsg = "We want to make sure its really you.In order to further verify your identity,enter the verification code that was send to " + email;
            return View();

        }
        [HttpPost]
        public ActionResult U_Verify(User user)
        {
            if (Session["UserEmail"] == null || Session["RandomNumber"] == null)
            {
                return Redirect("~/Accounts/SignIn");
            }
            if (Convert.ToInt32(Session["RandomNumber"]) != user.VerificationCode)
            {
                ViewBag.VerificationMsg = "You entered invalid verification code, please try again.";
                return View();
            }
            //string email = Session["FreelancerEmail"].ToString();
            int userId = Convert.ToInt32(Session["UserId"]);
            try
            {
                var user_info = db.Users.SingleOrDefault(f => f.UserId == userId);
                if (user_info == null)
                {
                    return Redirect("~/Accounts/SignIn");  
                }
                user_info.EmailVerifiedFlag = 1;
                db.SaveChanges();
                return Redirect("~/Users/ProfileInfo");
            }
            catch (Exception)
            {
                return Redirect("~/Accounts/SignIn");
            }
        }
    }
}
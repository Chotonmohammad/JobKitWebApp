using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobKitWebApp.Areas.Users.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Users/Settings
        public ActionResult MyProfile()
        {
            return View();
        }
    }
}
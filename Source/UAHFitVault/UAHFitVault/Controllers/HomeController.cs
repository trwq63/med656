using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UAHFitVault.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Physician")) {
                return Redirect("/Physician/Index");
            }
            else if (User.IsInRole("Experiment Administrator")) {
                return Redirect("/Experiment/Index");
            }
            else if (User.IsInRole("Patient")) {
                return Redirect("/Patient/Index");
            }
            else if (User.IsInRole("System Administrator")) {
                return Redirect("/Admin/AccountRequests");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.Helpers;

namespace UAHFitVault.Controllers
{
    [Authorization("ROLES_PHYSICIAN")]
    public class PhysicianController : Controller
    {
        // GET: Physician
        public ActionResult Index()
        {
            return View();
        }
    }
}
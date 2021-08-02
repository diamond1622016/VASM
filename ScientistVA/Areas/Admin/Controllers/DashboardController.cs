using System;
using System.Linq;
using System.Web.Mvc;
using BELibrary.Core.Entity;
using BELibrary.DbContext;

namespace ScientistVA.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.Element = "System";
            ViewBag.Feature = "DashBoard";
            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            //
            ViewBag.DocumentCount = 10;
            ViewBag.PatientCount = 10;
            ViewBag.PrescriptionCount = 01;
            ViewBag.ItemCount = 10;

            var now = DateTime.Now;
            //
            ViewBag.DocumentTodayCount = 10;
            ViewBag.DocumentMonthCount = 10;

            ViewBag.PatientTodayCount = 10;
            ViewBag.PatientMonthCount = 10;

            ViewBag.PrescriptionTodayCount = 10;
            ViewBag.PrescriptionMonthCount = 10;

            ViewBag.ItemTodayCount = 10;
            ViewBag.ItemMonthCount = 10;

            return View();
        }
    }
}
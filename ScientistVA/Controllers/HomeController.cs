using BELibrary.Core.Entity;
using BELibrary.DbContext;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScientistVA.Controllers
{
    public class HomeController : Controller
    {
        /* public ActionResult Index()
         {
             return View();
         }*/
        private readonly string KeyElement = "Home";
        // GET: Admin/Scientist
        public ActionResult Index()
        {
            ViewBag.Feature = "All Data";
            ViewBag.Element = KeyElement;

            //if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            //using (var workScope = new UnitOfWork(new ScientistDbContext()))
            //{
            //    var listData = workScope.Scientists.GetAll().ToList();
            //    return View(listData);
            //}

            return RedirectToAction("About", "Home");
        }
        /*public ActionResult get(Guid id)
        {
            ViewBag.isEdit = true;
            ViewBag.Feature = "View";
            ViewBag.Element = KeyElement;
            if (Request.Url != null)
            {
                ViewBag.BaseURL = Request.Url.LocalPath;

                ViewBag.BaseURL = string.Join("", Request.Url.Segments.Take(Request.Url.Segments.Length - 1));
            }

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var scientist = workScope.Scientists
                    .FirstOrDefault(x => x.Id == id);

                if (scientist != null)
                {
                    return View("ViewDetail", scientist); //Create
                }
                else
                {
                    return RedirectToAction("Create", "Scientist");
                }
            }
        }
        public ActionResult getAllVN()
        {
            ViewBag.Feature = "Vietnamese Scientists";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Scientists.Query(x => x.Nationality.ToLower() == "VietNam" || x.Email_domain.ToLower().Contains(".vn")).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult getAllAustr()
        {
            ViewBag.Feature = "Autralian Scientists";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Scientists.Query(x => x.Nationality.ToLower() == "Australia" || x.Email_domain.ToLower().Contains(".au")).ToList();
                return View("Index", listData);
            }
        }*/

        public ActionResult About()
        {
            ViewBag.Feature = "About";
            ViewBag.Element = "Scientist";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
    }
}
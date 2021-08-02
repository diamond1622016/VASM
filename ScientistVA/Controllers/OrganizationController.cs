using BELibrary.Core.Entity;
using BELibrary.DbContext;
using ScientistVA.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScientistVA.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: organize
        private readonly string KeyElement = "Organization";
        // GET: Scientist
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getAllVN()
        {
            ViewBag.Feature = "Vietnamese Organization";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Organization.Query(x => x.Country.ToLower().Contains("vietnam") || x.Email.ToLower().Contains(".vn")).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult getAllAU()
        {
            ViewBag.Feature = "Autralian Organization";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Organization.Query(x => x.Country.ToLower().Contains("australia") || x.Email.ToLower().Contains(".au")).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult Detail(int id)
        {
            return View();
        }
        public ActionResult get(Guid id)
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
                var organization = workScope.Organization
                    .FirstOrDefault(x => x.Id == id);

                if (organization != null)
                {
                    return View("Create", organization);
                }
                else
                {
                    return RedirectToAction("Create", "Organization");
                }
            }
        }
        public ActionResult getVNOrg(string type)
        {
            if (type == "University")
                ViewBag.Feature = "University";
            else if (type == "company")
                ViewBag.Feature = "Company";
            else
                ViewBag.Feature = "Institute";

            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Organization.Query(x => x.Business_type.Contains(type) && (x.Country.ToLower().Contains("vietnam"))).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult getAUOrg(string type)
        {
            if (type == "University")
                ViewBag.Feature = "University";
            else if (type == "company")
                ViewBag.Feature = "Company";
            else
                ViewBag.Feature = "Institute";

            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Organization.Query(x => x.Business_type.Contains(type) && (x.Country.ToLower().Contains("australia"))).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult getDetail(Guid id)
        {
            ViewBag.isEdit = true;
            ViewBag.Feature = "View";
            ViewBag.Element = KeyElement;
            if (Request.Url != null)
            {
                //ViewBag.BaseURL = Request.Url.LocalPath;

                //ViewBag.BaseURL = string.Join("", Request.Url.Segments.Take(Request.Url.Segments.Length - 1));
                ViewBag.BaseURL = "getAllVN";
            }

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var organization = workScope.Organization
                    .FirstOrDefault(x => x.Id == id);

                var listScientist = workScope.Scientists.Query(x => x.OrganizationId== id).ToList();
                ViewBag.listScientist = listScientist;

                return View("Detail", organization);
            }
        }
        public ActionResult Update(Guid id)
        {
            if (!CookiesManage.Logined())
            {
                return RedirectToAction("Login", "Account");
            }
            var user = CookiesManage.GetUser();


            if (!user.ScientistId.HasValue || user.ScientistId != id)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.isEdit = true;
            ViewBag.Feature = "Update";
            ViewBag.Element = KeyElement;
            if (Request.Url != null)
            {
                ViewBag.BaseURL = Request.Url.LocalPath;

                ViewBag.BaseURL = string.Join("", Request.Url.Segments.Take(Request.Url.Segments.Length - 1));
            }

            var businessType = new List<object>()
            {
                new
                {
                    Id = "university",
                    Name = "University"
                },
                new
                {
                   Id = "institute",
                    Name = "Institute"
                },
                new
                {
                   Id = "company",
                    Name = "Company"
                },
                new
                {
                   Id = "others",
                    Name = "Others"
                },
            };
            ViewBag.BusinessType = new SelectList(businessType, "Id", "Name");

            var ownershipType = new List<object>()
            {
                new
                {
                    Id = "public",
                    Name = "Public"
                },
                new
                {
                   Id = "private",
                    Name = "Private"
                },
                new
                {
                   Id = "others",
                    Name = "Others"
                },
            };
            ViewBag.OwnershipType = new SelectList(ownershipType, "Id", "Name");

            var interestedTopic = new List<object>()
            {
                new
                {
                    Id = "Machine Learning",
                    Name = "Machine Learning"
                },
                new
                {
                   Id = "Deep Learning",
                    Name = "Deep Learning"
                },
                new
                {
                   Id = "Reinforcement Learning",
                    Name = "Reinforcement Learning"
                },
                new
                {
                   Id = "Robotics",
                    Name = "Robotics"
                },
                new
                {
                   Id = "Natural Language Processing",
                    Name = "Natural Language Processing"
                },
                new
                {
                   Id = "Computer Vision",
                    Name = "Computer Vision"
                },
                new
                {
                   Id = "Internet of Things",
                    Name = "Internet of Things"
                },
                new
                {
                   Id = "Recommender Systems",
                    Name = "Recommender Systems"
                },
                new
                {
                   Id = "Others",
                    Name = "Others"
                },
            };
            ViewBag.InterestedTopic = new SelectList(interestedTopic, "Id", "Name");

            var nationality = new List<object>()
            {
                new
                {
                    Id = "VietNam",
                    Name = "VietNam"
                },
                new
                {
                   Id = "Australia",
                    Name = "Australia"
                },
            };
            ViewBag.Nationality = new SelectList(nationality, "Id", "Name");

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var organization = workScope.Organization
                    .FirstOrDefault(x => x.Id == id);

                if (organization != null)
                {
                    return View("Create", organization);
                }
                else
                {
                    return RedirectToAction("Create", "Scientist");
                }
            }
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult CreateOrEdit(BELibrary.Entity.Organization input, bool isEdit)
        {

            if (!CookiesManage.Logined())
            {
                return Json(new { status = false, mess = "Not logged in yet!" });
            }
            var user = CookiesManage.GetUser();


            if (!user.ScientistId.HasValue || user.ScientistId != input.Id)
            {
                return Json(new { status = false, mess = "Do not have enough permissions to perform this task!" });
            }


            try
            {
                if (isEdit) //update
                {
                    using (var workScope = new UnitOfWork(new ScientistDbContext()))
                    {
                        var elm = workScope.Organization.Get(input.Id);

                        if (elm != null) //update
                        {
                            //Che bien du lieu

                            //input.CreationTime = DateTime.Now;

                            elm = input;
                            //elm.IsDelete = false;

                            workScope.Organization.Put(elm, elm.Id);
                            workScope.Complete();

                            return Json(new { status = true, mess = "Update successful!" });
                        }
                        else
                        {
                            return Json(new { status = false, mess = "Does not exist " + KeyElement });
                        }
                    }
                }
                else //Thêm mới
                {
                    using (var workScope = new UnitOfWork(new ScientistDbContext()))
                    {
                        //Che bien du lieu
                        input.Id = Guid.NewGuid();
                        //input.IsDelete = false;
                        //input.CreationTime = DateTime.Now;

                        workScope.Organization.Add(input);
                        workScope.Complete();
                    }
                    return Json(new { status = true, mess = "Add successful!" + KeyElement });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    mess = "An error occurred: " + ex.Message
                });
            }
        }
    }
}
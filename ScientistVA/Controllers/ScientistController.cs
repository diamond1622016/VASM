using BELibrary.Core.Entity;
using BELibrary.DbContext;
using BELibrary.Entity;
using ScientistVA.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace ScientistVA.Controllers
{
    public class ScientistController : Controller
    {
        private readonly string KeyElement = "Scientist";
        // GET: Scientist
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getAllVN()
        {
            ViewBag.Feature = "All Vietnamese Scientists";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Scientists.Query(x => x.Nationality.ToLower().Contains("vietnam") || x.Email_domain.ToLower().Contains(".vn")).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult getAllAustr()
        {
            ViewBag.Feature = "All Autralian Scientists";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Scientists.Query(x => x.Nationality.ToLower().Contains("australia") || x.Email_domain.ToLower().Contains(".au")).ToList();
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
                //ViewBag.BaseURL = Request.Url.LocalPath;

                //ViewBag.BaseURL = string.Join("", Request.Url.Segments.Take(Request.Url.Segments.Length - 1));
                ViewBag.BaseURL = "SearchAll";
            }

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var scientist = workScope.Scientists
                    .FirstOrDefault(x => x.Id == id);

                if (scientist != null)
                {
                    return View("ViewDetail", scientist);
                }
                else
                {
                    return RedirectToAction("Create", "Scientist");
                }
            }
        }
        public ActionResult getAllPapers(Guid id)
        {
            ViewBag.isEdit = true;
            ViewBag.Feature = "View all publications";
            ViewBag.Element = KeyElement;
            if (Request.Url != null)
            {
                //ViewBag.BaseURL = Request.Url.LocalPath;

                //ViewBag.BaseURL = string.Join("", Request.Url.Segments.Take(Request.Url.Segments.Length - 1));
                ViewBag.BaseURL = "SearchAll";
            }

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var scientist = workScope.Scientists
                    .FirstOrDefault(x => x.Id == id);

                var listData = workScope.Paper.Query(x => x.ScientistId == id).ToList();
                if (listData != null)
                {
                    ViewBag.ListPaper = listData;
                    return View("ViewDetailPaper", scientist);
                }
                else
                {
                    return RedirectToAction("ViewDetail", "Scientist");
                }
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


            var positions = new List<object>()
            {
                new
                {
                    Id = "lecturer",
                    Name = "Lecturer"
                },
                new
                {
                   Id = "Software developer",
                    Name = "Software developer"
                },
                new
                {
                   Id = "Manager",
                    Name = "Manager"
                },
                new
                {
                   Id = "CEO",
                    Name = "Chief Executive Officer"
                },
                new
                {
                   Id = "Project manager",
                    Name = "Project manager"
                },
                new
                {
                   Id = "Team leader",
                    Name = "Tem leader"
                },
                new
                {
                   Id = "Others",
                    Name = "Others"
                },
            };


            ViewBag.Positions = new SelectList(positions, "Id", "Name");

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
                var scientist = workScope.Scientists
                    .FirstOrDefault(x => x.Id == id);

                if (scientist != null)
                {
                    ViewBag.CountryTypeSelected = scientist.Nationality;
                    ViewBag.PositionTypeSelected = scientist.Position;

                    var slInterested_topics = !string.IsNullOrEmpty(scientist.Interested_topics) ? scientist.Interested_topics.Split(',') : new string[0];

                    var org = workScope.Organization.FirstOrDefault(x => x.Id == scientist.OrganizationId);

                    ViewBag.OrgName = (org != null) ? org.Name : "";
                    ViewBag.SelectedInterestedTopic = slInterested_topics.ToList();

                    return View("Create", scientist);
                }
                else
                {
                    return RedirectToAction("Create", "Scientist");
                }
            }
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult CreateOrEdit(Scientist input, bool isEdit)
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
                        var elm = workScope.Scientists.Get(input.Id);

                        if (elm != null) //update
                        {
                            //Che bien du lieu

                            //input.CreationTime = DateTime.Now;
                            input.Type = elm.Type;
                            elm = input;
                            //elm.IsDelete = false;

                            workScope.Scientists.Put(elm, elm.Id);
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
                        input.Type = "1";
                        workScope.Scientists.Add(input);
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
        
        [HttpGet]
        public JsonResult GetJson(string query)
        {
            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var scientists = workScope.Scientists.Query(x => x.Name.Contains(query)).Select(x => new
                {
                    value = x.Name,
                    data = x.Id
                }).ToList();

                return Json(new
                {
                    suggestions = scientists
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SearchAll(string query, int? page, string country)
        {
            if(country!=null)
                ViewBag.Country = country;

            if (country=="vn")
                ViewBag.Feature = "Vietnamese Scientists";
            else if(country == "au")
                ViewBag.Feature = "Australian Scientists";
            else
                ViewBag.Feature = "All Scientists";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            if (query == "")
            {
                query = null;
            }

            ViewBag.QueryData = query;
            var pageNumber = (page ?? 1);
            const int pageSize = 10;
            var listData = new List<Scientist>();
            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                //var listData = workScope.Scientists.Query(x => !x.IsDelete).OrderByDescending(x => x.ModifiedDate).ToList();
                if(country=="vn")
                    listData = workScope.Scientists.Query(x => x.Nationality.ToLower().Contains("vietnam") || x.Email_domain.ToLower().Contains(".vn")).ToList();
                else if (country == "au")
                    listData = workScope.Scientists.Query(x => x.Nationality.ToLower().Contains("australia") || x.Email_domain.ToLower().Contains(".au")).ToList();
                else
                    listData = workScope.Scientists.Query(x => x.Nationality!="").ToList();
                if (query == null)
                {
                    ViewBag.Total = listData.Count();
                    return View("Index", listData.ToPagedList(pageNumber, pageSize));
                }

                var q = (from mt in listData
                         where (!string.IsNullOrEmpty(query) &&
                                (mt.Name.ToLower().Contains(query.ToLower())
                                 || !string.IsNullOrEmpty(mt.Affiliation) && mt.Affiliation.ToLower().Contains(query.ToLower())
                                 || !string.IsNullOrEmpty(mt.Interested_topics) && mt.Interested_topics.ToLower().Contains(query.ToLower())
                                 || !string.IsNullOrEmpty(mt.Nationality) && mt.Nationality.ToLower().Contains(query.ToLower())
                                 || !string.IsNullOrEmpty(mt.Position) && mt.Position.ToLower().Contains(query.ToLower())))

                         select mt).AsQueryable();

                ViewBag.Total = q.Count();
                return View("Index", q.ToPagedList(pageNumber, pageSize));
            }
        }
        
    }
}
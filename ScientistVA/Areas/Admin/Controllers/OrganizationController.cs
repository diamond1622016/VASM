using BELibrary.Core.Entity;
using BELibrary.DbContext;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using ScientistVA.Areas.Admin.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScientistVA.Areas.Admin.Controllers
{
    public class OrganizationController : BaseController
    {
        private readonly string KeyElement = "Organization";
        // GET: Admin/Scientist
        public ActionResult Index()
        {
            ViewBag.Feature = "All Data";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Organization.GetAll().ToList();
                return View(listData);
            }
        }
        public ActionResult getAllVN()
        {
            ViewBag.Feature = "All Vietnamese AI Organization";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Organization.Query(x => x.Country.ToLower().Contains("vietnam")).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult getAllAustr()
        {
            ViewBag.Feature = "All Autralian AI Organization";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Organization.Query(x => x.Country.ToLower().Contains("australia")).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult Create()
        {
            ViewBag.Feature = "Add new";
            ViewBag.Element = KeyElement;
            if (Request.Url != null)
                ViewBag.BaseURL = string.Join("", Request.Url.Segments.Take(Request.Url.Segments.Length - 1));

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

            ViewBag.IsEdit = false;
            return View();
        }
        public ActionResult Update(Guid id)
        {
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

                ViewBag.BusinessTypeSelected = organization.Business_type;
                ViewBag.OwnershipTypeSelected = organization.Ownership_type;
                ViewBag.NationalitySelected = organization.Country;

                if (organization != null)
                {
                    var slInterested_topics = !string.IsNullOrEmpty(organization.Interested_topics) ? organization.Interested_topics.Split(',') : new string[0];

                    ViewBag.SelectedInterestedTopic = slInterested_topics.ToList();

                    return View("Create", organization);
                }
                else
                {
                    return RedirectToAction("Create", "Organization");
                }
            }
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult CreateOrEdit(BELibrary.Entity.Organization input, bool isEdit)
        {

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

        [HttpPost]
        public JsonResult Del(Guid id)
        {
            try
            {
                using (var workScope = new UnitOfWork(new ScientistDbContext()))
                {
                    var elm = workScope.Scientists.Get(id);
                    if (elm != null)
                    {
                        //del
                        workScope.Scientists.Remove(elm);
                        workScope.Complete();
                        return Json(new { status = true, mess = "Delete succesfull! " + KeyElement });
                    }
                    else
                    {
                        return Json(new { status = false, mess = "Donot exist " + KeyElement });
                    }
                }
            }
            catch
            {
                return Json(new { status = false, mess = "Fail!" });
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

        private Stream CreateExcelFile(Stream stream = null)
        {
            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var list = workScope.Organization.GetAll().ToList();

                using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
                {
                    // Tạo author cho file Excel
                    excelPackage.Workbook.Properties.Author = "VTV";
                    // Tạo title cho file Excel
                    excelPackage.Workbook.Properties.Title = "Organizations";
                    // thêm tí comments vào làm màu 
                    excelPackage.Workbook.Properties.Comments = "All Organizations";
                    // Add Sheet vào file Excel
                    excelPackage.Workbook.Worksheets.Add("Organizations");
                    // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                    var workSheet = excelPackage.Workbook.Worksheets[1];
                    // Đỗ data vào Excel file
                    //workSheet.Cells.Style.WrapText = true;
                    //workSheet.Cells[0, 0].Value = "VIETNAM AUSTRALIA SCIENTISTS";
                    workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);

                    //BindingFormatForExcel(workSheet, list);

                    excelPackage.Save();
                    return excelPackage.Stream;
                }
            }

        }

        [HttpGet]
        public ActionResult Export()
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Organizations.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index :D
            return RedirectToAction("Index");
        }
    }
}
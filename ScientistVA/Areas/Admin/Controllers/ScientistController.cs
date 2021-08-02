using BELibrary.Core.Entity;
using BELibrary.DbContext;
using BELibrary.Entity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ScientistVA.Areas.Admin.Controllers
{
    public class ScientistController : BaseController
    {
        private readonly string KeyElement = "Scientist";
        // GET: Admin/Scientist
        public ActionResult Index()
        {
            ViewBag.Feature = "VietNamese Data";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                //var listData = workScope.Scientists.GetAll().ToList();
                var listData = workScope.Scientists.Query(x => (x.Nationality.ToLower().Contains("vietnam") || x.Email_domain.ToLower().Contains(".vn"))).ToList();
                return View(listData);
            }
        }

        [HttpGet]
        public JsonResult GetJson(string query)
        {
            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var orgs = workScope.Organization.Query(x => x.Name.Contains(query)).Select(x => new
                {
                    value = x.Name,
                    data = x.Id
                }).ToList();

                return Json(new
                {
                    suggestions = orgs
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getVNFrom(string type)
        {
            if(type=="1")
                ViewBag.Feature = "Scholar Vietnamese Scientists";
            else
                ViewBag.Feature = "Registration Vietnamese Scientists";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Scientists.Query(x => x.Type.Contains(type) && (x.Nationality.ToLower().Contains("vietnam") || x.Email_domain.ToLower().Contains(".vn"))).ToList();
                return View("Index", listData);
            }
        }
        public ActionResult getAUFrom(string type)
        {
            if (type == "1")
                ViewBag.Feature = "Scholar Australian Scientists";
            else
                ViewBag.Feature = "Registration Australian Scientists";
            ViewBag.Element = KeyElement;

            if (Request.Url != null) ViewBag.BaseURL = Request.Url.LocalPath;

            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var listData = workScope.Scientists.Query(x => x.Type.Contains(type) && (x.Nationality.ToLower().Contains("australia") || x.Email_domain.ToLower().Contains(".au"))).ToList();
                return View("Index", listData);
            }
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
        public ActionResult Create()
        {
            ViewBag.Feature = "Add new";
            ViewBag.Element = KeyElement;
            if (Request.Url != null)
                ViewBag.BaseURL = string.Join("", Request.Url.Segments.Take(Request.Url.Segments.Length - 1));

            ViewBag.SelectedInterestedTopic = new List<string>();

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
            ViewBag.OrgName = "";
            ViewBag.IsEdit = false;
            //var sc = new Scientist { Type = "1" };
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
                            input.Type = elm.Type;
                            //input.CreationTime = DateTime.Now;

                            elm = input;
                            //elm.IsDelete = false;

                            workScope.Scientists.Put(elm, elm.Id);
                            workScope.Complete();

                            return Json(new { status = true, mess = "Updated successfully" });
                        }
                        else
                        {
                            return Json(new { status = false, mess = "Not exist " + KeyElement });
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
                    return Json(new { status = true, mess = "Inserted successfully" + KeyElement });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    mess = "There is an error: " + ex.Message
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
                        return Json(new { status = true, mess = "Deleted successfully " + KeyElement });
                    }
                    else
                    {
                        return Json(new { status = false, mess = "Not exist " + KeyElement });
                    }
                }
            }
            catch
            {
                return Json(new { status = false, mess = "Failed" });
            }
        }


        private Stream CreateExcelFile(Stream stream = null)
        {
            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var list = workScope.Scientists.GetAll().ToList();

                using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
                {
                    // Tạo author cho file Excel
                    excelPackage.Workbook.Properties.Author = "VTV";
                    // Tạo title cho file Excel
                    excelPackage.Workbook.Properties.Title = "Scientists";
                    // thêm tí comments vào làm màu 
                    excelPackage.Workbook.Properties.Comments = "All Scientists";
                    // Add Sheet vào file Excel
                    excelPackage.Workbook.Worksheets.Add("Scientists");
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

        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<Scientist> scientists)
        {
            
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header

            int z = 8;

            //worksheet.Cells[z, 1].Value = "Mã sản phẩm";
            //worksheet.Cells[z, 2].Value = "Tên sản phẩm";
            //worksheet.Cells[z, 3].Value = "Số lượng bán";
            //worksheet.Cells[z, 4].Value = "Thành tiền";

            // Lấy range vào tạo format cho range đó ở đây là từ A7 tới D7
            using (var range = worksheet.Cells["A0:D0"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;

                range.Style.Font.Color.SetColor(Color.DarkViolet);
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 10));
                // Set Border
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }

            // Đỗ dữ liệu từ list vào 
            //for (int i = 0; i < scientists.Count; i++)
            //{
            //    var item = scientists[i];
            //    worksheet.Cells[i + 1 + z, 1].Value = item.Id;
            //    worksheet.Cells[i + 1 + z, 2].Value = item.Name;
            //    worksheet.Cells[i + 1 + z, 3].Value = item.Affiliation;
            //    worksheet.Cells[i + 1 + z, 4].Value = item.Citedby;
            //    //Format lại color nế+6u như thỏa điều kiện
            //    //if (item.Revenue > 10000050)
            //    //{
            //    //    Ở đây chúng ta sẽ format lại theo dạng fromRow, fromCol, toRow, toCol
            //    //    using (var range = worksheet.Cells[i + 2, 1, i + 2, 6])
            //    //    {
            //    //        Format text đỏ và đậm
            //    //        range.Style.Font.Color.SetColor(Color.Red);
            //    //        range.Style.Font.Bold = true;
            //    //    }
            //    //}

            //}
            //// Format lại định dạng xuất ra ở cột Money
            //worksheet.Cells[2 + z, 4, scientists.Count + 4 + z, 4].Style.Numberformat.Format = "#,##0";
            //// fix lại width của column với minimum width là 15
            //worksheet.Cells[1 + z, 1, scientists.Count + 5 + z, 4].AutoFitColumns(15);

            //// Thực hiện tính theo formula trong excel
            //// Hàm Sum 

            //worksheet.Cells[scientists.Count + 4 + z, 3].Value = "Tổng SL bán:";
            //worksheet.Cells[scientists.Count + 4 + z, 4].Formula = "SUM(C8:C" + (scientists.Count + 1 + z) + ")";


            //// Tổng doanh thu
            //worksheet.Cells[scientists.Count + 5 + z, 3].Value = "Tổng doanh thu: ";
            //worksheet.Cells[scientists.Count + 5 + z, 3].Style.Font.Color.SetColor(Color.Red);
            //worksheet.Cells[scientists.Count + 5 + z, 4].Style.Numberformat.Format = "#,##0";
            //worksheet.Cells[scientists.Count + 5 + z, 4].Formula = "SUM(D8:D" + (scientists.Count + 1 + z) + ")";
            //worksheet.Cells[scientists.Count + 5 + z, 4].Style.Font.Color.SetColor(Color.Red);


            //// Infor 
            //worksheet.Cells[2, 1, 2, 4].Merge = true;
            //var cellTitleInf = worksheet.Cells[2, 1, 2, 4];
            //cellTitleInf.Value = "Shop Điện thoại Minh Anh";
            //cellTitleInf.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //cellTitleInf.Style.Font.Color.SetColor(Color.Red);
            //cellTitleInf.Style.Border.BorderAround(ExcelBorderStyle.Double);


            ////worksheet.Cells[listItems.Count + 6 + 3, 3].Value = "Thống kê từ: ";
            ////worksheet.Cells[listItems.Count + 6 + 3, 3].Style.Font.Color.SetColor(Color.Blue);
            ////worksheet.Cells[listItems.Count + 6 + 3, 3].AutoFitColumns();
            ////worksheet.Cells[listItems.Count + 6 + 3, 4].Value = input.StartTime+ " - "+ input.EndTime;
            ////worksheet.Cells[listItems.Count + 6 + 3, 4].AutoFitColumns();
            ////worksheet.Cells[listItems.Count + 6 + 3, 4].Style.Font.Color.SetColor(Color.Red);



            //worksheet.Cells[3 + 1, 3].Value = "Người xuất: ";
            //worksheet.Cells[3 + 1, 3].Style.Font.Color.SetColor(Color.Blue);
            //worksheet.Cells[3 + 1, 4].Value = GetCurrentUser().FullName;
            //worksheet.Cells[3 + 1, 4].AutoFitColumns();
            //worksheet.Cells[3 + 1, 4].Style.Font.Color.SetColor(Color.Red);


            //worksheet.Cells[4 + 1, 3].Value = "Ngày xuất: ";
            //worksheet.Cells[4 + 1, 3].Style.Font.Color.SetColor(Color.Blue);
            //worksheet.Cells[4 + 1, 4].Value = DateTime.Now.ToString("dd/MM/yyy HH:mm:ss");
            //worksheet.Cells[4 + 1, 4].AutoFitColumns();
            //worksheet.Cells[4 + 1, 4].Style.Font.Color.SetColor(Color.Red);



            //worksheet.Cells[5 + 1, 3, 5 + 1, 4].Merge = true;
            //var cellTimeInf = worksheet.Cells[5 + 1, 3, 5 + 1, 4];
            //cellTimeInf.Value = input.StartTime + " - " + input.EndTime;
            //cellTimeInf.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //cellTimeInf.Style.Font.Color.SetColor(Color.Red);
            //cellTimeInf.Style.Border.BorderAround(ExcelBorderStyle.Thin);
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Scientists.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index :D
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ImportEx(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.  
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.  
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace  
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }
                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.  
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();  
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    using (var workScope = new UnitOfWork(new ScientistDbContext()))
                    {
                        Guid id = Guid.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
                        var scientist = workScope.Scientists.FirstOrDefault(x => x.Id == id);
                        if (scientist == null)
                        {
                            Scientist input = new Scientist();
                            input.Id = Guid.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
                            input.Type = input.Working_plan = ds.Tables[0].Rows[i]["Type"].ToString(); 
                            input.Academic_pos = ds.Tables[0].Rows[i]["Academic pos"].ToString();
                            input.Affiliation = ds.Tables[0].Rows[i]["Affiliation"].ToString();
                            input.AFF_Desc = ds.Tables[0].Rows[i]["AFF Desc"].ToString();
                            input.Bio_sketch = ds.Tables[0].Rows[i]["Bio sketch"].ToString();
                            input.Citedby = ds.Tables[0].Rows[i]["Citedby"].ToString();
                            input.Citedby5y = ds.Tables[0].Rows[i]["Citedby5y"].ToString();
                            input.Cites_per_year = ds.Tables[0].Rows[i]["Cites per year"].ToString();
                            input.Coauthors = ds.Tables[0].Rows[i]["Coauthors"].ToString();
                            input.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                            input.Email_domain = ds.Tables[0].Rows[i]["Email domain"].ToString();
                            input.HIndex = ds.Tables[0].Rows[i]["HIndex"].ToString();
                            input.Hindex5y = ds.Tables[0].Rows[i]["Hindex5y"].ToString();
                            input.I10Index = ds.Tables[0].Rows[i]["I10Index"].ToString();
                            input.I10index5y = ds.Tables[0].Rows[i]["I10index5y"].ToString();
                            input.Interested_topics = ds.Tables[0].Rows[i]["Interested topics"].ToString();
                            input.Interests = ds.Tables[0].Rows[i]["Interests"].ToString();
                            input.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                            input.Nationality = ds.Tables[0].Rows[i]["Nationality"].ToString();
                            input.OrganizationId = Guid.Parse(ds.Tables[0].Rows[i]["OrganizationId"].ToString());
                            input.PaperList = ds.Tables[0].Rows[i]["PaperList"].ToString();
                            input.Paper_num = ds.Tables[0].Rows[i]["Paper_num"].ToString();
                            input.Personal_web_link = ds.Tables[0].Rows[i]["Personal web link"].ToString();
                            input.Position = ds.Tables[0].Rows[i]["Position"].ToString();
                            input.Qualification = ds.Tables[0].Rows[i]["Qualification"].ToString();
                            input.Scholar_id = ds.Tables[0].Rows[i]["Scholar id"].ToString();
                            input.Url_picture = ds.Tables[0].Rows[i]["Url picture"].ToString();
                            input.Working_plan = ds.Tables[0].Rows[i]["Working plan"].ToString();

                            workScope.Scientists.Add(input);
                            workScope.Complete();
                        }
                    }
                }
                ViewBag.Data = ds.Tables[0];
            }
            return View("Import");
           // return Json(new { status = true, mess = "Updated successfully" });
        }

        [HttpGet]
        public ActionResult Import()
        {
            // Redirect về luôn trang index :D
            return View();
        }

        [ActionName("Importexcel")]
        [HttpPost]
        public ActionResult Importexcel1()
        {
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                string query = null;
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["FileUpload1"].SaveAs(path1);
                    if (extension == ".csv")
                    {
                        DataTable dt = ConvertCSVtoDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook  
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }

                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";

                }

            }

            return View("Import");
        }

        public DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }


            return dt;
        }

        public DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch
            {
            }
            finally
            {

                oledbConn.Close();
            }

            return dt;

        }

    }
}
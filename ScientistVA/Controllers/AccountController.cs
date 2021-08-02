using BELibrary.Core.Entity;
using BELibrary.Core.Utils;
using BELibrary.DbContext;
using BELibrary.Entity;
using BELibrary.Utils;
using ScientistVA.Handler;
using ScientistVA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ScientistVA.Controllers
{
    public class AccountController : Controller
    {
        private bool IsValidRecaptcha(string recaptcha)
        {
            // https://www.imic.edu.vn/tin-tuc-cong-nghe/27886/nhung-recaptcha-2-vao-asp-net-mvc.html?fbclid=IwAR1ELs2_2oAO8qXmhTfb-_eXS872szyq8i_GExRHFaTZI02l2OqHY1g3PUc
            if (string.IsNullOrEmpty(recaptcha))
            {
                return false;
            }
            var secretKey = "6Lcc8dAbAAAAAJwwOWuZL0YSAstKuHVURuiAAaCb";//Mã bí mật
            string remoteIp = Request.ServerVariables["REMOTE_ADDR"];
            string myParameters = String.Format("secret={0}&response={1}&remoteip={2}", secretKey, recaptcha, remoteIp);
            RecaptchaResult captchaResult;
            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var json = wc.UploadString("https://www.google.com/recaptcha/api/siteverify", myParameters);
                var js = new DataContractJsonSerializer(typeof(RecaptchaResult));
                var ms = new MemoryStream(Encoding.ASCII.GetBytes(json));
                captchaResult = js.ReadObject(ms) as RecaptchaResult;
                if (captchaResult != null && captchaResult.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (IsValidRecaptcha(Request["g-recaptcha-response"]))
            {
                ViewBag.Status = "Valid!";
            }
            else
            {
                ViewBag.Status = "Invalid!";
            }
            return View();
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login(string returnUrl = "")
        {
            if (CookiesManage.Logined())
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Register(string returnUrl = "")
        {
            if (CookiesManage.Logined())
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateInput(true)]
        public JsonResult CheckLogin(LoginModel model)
        {
            using (var workScope = new UnitOfWork(new ScientistDbContext()))
            {
                var account = workScope.Accounts.ValidFeAccount(model.Username, model.Password);

                if (HttpContext.Request.Url != null)
                {
                    var host = HttpContext.Request.Url.Authority;
                    if (account != null)
                    {
                        //đăng nhập thành công
                        var cookieClient = account.UserName + "|" + host.ToLower() + "|" + account.Id;
                        var decodeCookieClient = CryptorEngine.Encrypt(cookieClient, true);
                        var userCookie = new HttpCookie(CookiesKey.Client)
                        {
                            Value = decodeCookieClient,
                            Expires = DateTime.Now.AddDays(30)
                        };
                        HttpContext.Response.Cookies.Add(userCookie);
                        return Json(new { status = true, mess = "Login succefull!" });
                    }
                    else
                    {
                        return Json(new { status = false, mess = "Incorrect username and password" });
                    }
                }
                else
                {
                    return Json(new { status = false, mess = "Incorrect username and password" });
                }
            }
        }

        [HttpPost]
        [ValidateInput(true)]
        public JsonResult Register(Account us, string rePassword, string scholarLink)
        {
            if (IsValidRecaptcha(Request["g-recaptcha-response"]))
            {
                ViewBag.Status = " Valid Captcha!";

                string googleScholarID = "";
                if (us.Password != rePassword)
                {
                    return Json(new { status = false, mess = "The password incorrect" });
                }
                if (scholarLink != "" && scholarLink.Contains("scholar.google.com"))
                {
                    int lastindex = scholarLink.LastIndexOf("user=") + 5;
                    googleScholarID = scholarLink.Substring(lastindex, 12); // scholarID.Length - lastindex

                }
                using (var workScope = new UnitOfWork(new ScientistDbContext()))
                {
                    var account = workScope.Accounts.FirstOrDefault(x => x.UserName.ToLower() == us.UserName.ToLower());
                    if (account == null)
                    {
                        try
                        {
                            var scientistCurr = workScope.Scientists.FirstOrDefault(x => x.Scholar_id.ToLower() == googleScholarID.ToLower());
                            if (scientistCurr != null)
                            {
                                var accountCurr = workScope.Accounts.FirstOrDefault(x => x.ScientistId == scientistCurr.Id);
                                // Nếu Account này chưa đăng ký Scholar
                                if (accountCurr == null)
                                {
                                    var passwordFactory = us.Password + VariableExtensions.KeyCryptorClient;
                                    var passwordCryptor = CryptorEngine.Encrypt(passwordFactory, true);

                                    us.Password = passwordCryptor;
                                    us.Role = RoleKey.Client;
                                    us.ScientistId = scientistCurr.Id;
                                    us.LinkAvatar = scientistCurr.Url_picture;
                                    us.Id = Guid.NewGuid();
                                    workScope.Accounts.Add(us);
                                    workScope.Complete();
                                }
                                // Nếu đã đăng ký Scholar rồi
                                else
                                {
                                    return Json(new { status = false, mess = "This Google Scholar id was already registered by another account!" });
                                }
                            }
                            // Nếu hệ thống chưa có Scholar ID này thì sinh mới
                            else
                            {
                                var avt = "/Content/images/avatar.png";

                                var scientistId = Guid.NewGuid();
                                var scientist = new Scientist
                                {
                                    Id = scientistId,
                                    Name = us.FullName,
                                    Url_picture = avt

                                };
                                workScope.Scientists.Add(scientist);
                                workScope.Complete();

                                var passwordFactory = us.Password + VariableExtensions.KeyCryptorClient;
                                var passwordCryptor = CryptorEngine.Encrypt(passwordFactory, true);

                                us.Password = passwordCryptor;
                                us.Role = RoleKey.Client;
                                us.ScientistId = scientistId;
                                us.LinkAvatar = avt;
                                us.Id = Guid.NewGuid();
                                workScope.Accounts.Add(us);
                                workScope.Complete();
                            }
                            //Login luon
                            if (HttpContext.Request.Url != null)
                            {
                                var host = HttpContext.Request.Url.Authority;

                                var cookieClient = us.UserName + "|" + host.ToLower() + "|" + us.Id;
                                var decodeCookieClient = CryptorEngine.Encrypt(cookieClient, true);
                                var userCookie = new HttpCookie(CookiesKey.Client)
                                {
                                    Value = decodeCookieClient,
                                    Expires = DateTime.Now.AddDays(30)
                                };
                                HttpContext.Response.Cookies.Add(userCookie);
                                return Json(new { status = true, mess = "Successful registration" });
                            }
                            else
                            {
                                return Json(new { status = false, mess = "Add failed" });
                            }
                        }
                        catch (Exception)
                        {
                            return Json(new { status = false, mess = "Add failed" });
                        }
                    }
                    else
                    {
                        return Json(new { status = false, mess = "Username already exists!" });
                    }
                }
            }
            else
            {
                ViewBag.Status = "Invalid!";
                return Json(new { status = false, mess = "Invalid Captcha" });
            }

            
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var nameCookie = Request.Cookies[CookiesKey.Client];
            if (nameCookie == null) return RedirectToAction("Index", "Home");
            var myCookie = new HttpCookie(CookiesKey.Client)
            {
                Expires = DateTime.Now.AddDays(-1d)
            };
            Response.Cookies.Add(myCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}
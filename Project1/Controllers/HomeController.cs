using Project1.Models.DB;
using qcloudsms_csharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        EntityDB entity = new EntityDB();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string phone)
        {
            user user = entity.user.FirstOrDefault(p => p.phone == phone);
            if (user != null)
            {
                HttpContext.Session["login"] = true;
                return RedirectToAction("Video", "Home");
            }
            return RedirectToAction("Register", "Home");
        }
        public ActionResult Qr()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port + "/Home/Index";
            ViewBag.url = url;
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string phone, string name, string school, string code)
        {
            user user = entity.user.FirstOrDefault(p => p.phone == phone);
            if (user == null)
            {
                phonecode phonecode = entity.phonecode.FirstOrDefault(p => p.phone == phone);
                if (phonecode.code == code)
                {
                    user new_user = new user()
                    {
                        name = name,
                        phone = phone,
                        school = school,
                        sys_datetime = DateTime.Now,
                    };
                    entity.user.Add(new_user);
                    if (entity.SaveChanges() > 0)
                    {
                        HttpContext.Session["login"] = true;
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("系统发生错误，请稍后重试", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("验证码无效", JsonRequestBehavior.AllowGet);
            }
            return Json("该手机号已被注册", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SendCode(string phone)
        {
            Random num = new Random();
            int code = num.Next(1000, 9999);
            phonecode codeModel = entity.phonecode.FirstOrDefault(p => p.phone == phone);
            if (codeModel == null)
            {
                phonecode newCodeModel = new phonecode()
                {
                    code = code.ToString(),
                    phone = phone,
                    sys_datetime = DateTime.Now,
                };
                entity.phonecode.Add(newCodeModel);
            }
            else
            {
                codeModel.code = code.ToString();
            }
            SmsSingleSender ssender = new SmsSingleSender(1400078324, "b6bcf068fb3ef4b611833bffb0181aaa");
            var result = ssender.sendWithParam("86", phone, 100278, new[] { code.ToString(), "10" }, null, "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信
            Console.WriteLine(result);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Video()
        {
            if (HttpContext.Session["login"] == null)
            {
                return RedirectToAction("Register", "Home");
            }
            string url = entity.setting.FirstOrDefault(p => p.key == "url").value;
            ViewBag.url = url;
            return View();
        }
        public ActionResult UserList()
        {
            List<user> list = entity.user.ToList();
            ViewBag.url = entity.setting.FirstOrDefault(p => p.key == "url").value;
            return View(list);
        }
        public ActionResult SetUrl(string url)
        {
            setting setting = entity.setting.FirstOrDefault(p => p.key == "url");
            setting.value = url;
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
    }
}
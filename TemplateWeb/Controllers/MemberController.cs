using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Extension;
using TemplateWeb.Models.Account;
using TemplateWeb.Models.DB;

namespace TemplateWeb.Controllers
{
    public class MemberController : Controller
    {
        EntityDB entity = new EntityDB();
        public ActionResult Index()
        {
            return View();
        }
        #region 登陆
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string phone, string password)
        {
            account_member member = entity.account_member.FirstOrDefault(p => p.phone == phone);
            if (member != null && DESTool.Encrypt(password) == member.password)
            {
                if (member.enable == true)
                {
                    MemberModel model = new MemberModel()
                    {
                        phone = member.phone,
                        password = member.password,
                        isAuth = true,
                    };
                    HttpCookie cookie = new HttpCookie("tpadmin");
                    string cookieStr = JsonConvert.SerializeObject(model);
                    cookie.Value = DESTool.Encrypt(cookieStr);
                    HttpContext.Response.AppendCookie(cookie);
                    return Json(true, JsonRequestBehavior.AllowGet); ;
                }
                else
                {
                    return Json("账号已被停用", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("账号或密码错误", JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie("tpadmin");
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.AppendCookie(cookie);
            return RedirectToAction("Login", "Admin");
        }
        #endregion
    }
}
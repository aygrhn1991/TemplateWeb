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
    [MemberAuthorize]
    public class MemberController : Controller
    {
        EntityDB entity = new EntityDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
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
                    HttpContext.Session["tpmember"] = member;
                    return Json(true, JsonRequestBehavior.AllowGet);
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
            HttpContext.Session.Remove("tpmember");
            return RedirectToAction("Index", "Home");
        }
        #endregion
        public ActionResult Setting()
        {
            return View();
        }
    }
}
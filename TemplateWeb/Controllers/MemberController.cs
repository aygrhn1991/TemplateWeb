using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Component;
using TemplateWeb.Extension;
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
        #region 注册
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(string phone, string password,string code)
        {
            account_member account_member = entity.account_member.FirstOrDefault(p => p.phone == phone);
            if (account_member == null)
            {
                account_code account_code = entity.account_code.FirstOrDefault(p => p.phone == phone);
                if (account_code.code == code)
                {

                }
                return Json("验证码错误");



                account_member member = new account_member();
                member.phone = phone;
                member.password = DESTool.Encrypt(password);
                member.enable = true;
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
            return Json("账号已被注册", JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult Setting()
        {
            return View();
        }
    }
}
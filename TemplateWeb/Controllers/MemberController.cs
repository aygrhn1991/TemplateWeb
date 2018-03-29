using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Component;
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
        [AllowAnonymous]
        public ActionResult SendSMSCode(string phone)
        {
            SMSTool tool = new SMSTool();
            string code = tool.CreateCode(phone);
            string expireTime = tool.expireTime.ToString();
            bool codeResult = tool.SendCode(phone, 100278, new string[] { code, expireTime });
            if (codeResult == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("验证码发送失败", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(string phone, string password, string code)
        {
            account_member member = entity.account_member.FirstOrDefault(p => p.phone == phone);
            if (member == null)
            {
                SMSTool tool = new SMSTool();
                bool codeResult = tool.CheckCode(phone, code);
                if (codeResult == true)
                {
                    bool memberResult = MemberManager.CreateMember(phone, password);
                    if (memberResult == true)
                    {
                        account_member new_account_member = entity.account_member.FirstOrDefault(p => p.phone == phone);
                        HttpContext.Session["tpmember"] = new_account_member;
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    return Json("用户创建失败", JsonRequestBehavior.AllowGet);
                }
                return Json("验证码无效");
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
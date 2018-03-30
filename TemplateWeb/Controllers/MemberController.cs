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
                    account_member new_account_member = new account_member()
                    {
                        enable = true,
                        password = DESTool.Encrypt(password),
                        phone = phone,
                        sys_datetime = DateTime.Now,
                    };
                    entity.account_member.Add(new_account_member);
                    if (entity.SaveChanges() > 0)
                    {
                        HttpContext.Session["tpmember"] = new_account_member;
                        MessageTool.SendMessage(new_account_member.id, "系统通知", "恭喜您注册成功！");
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    return Json("用户创建失败", JsonRequestBehavior.AllowGet);
                }
                return Json("验证码无效");
            }
            return Json("账号已被注册", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 设置
        #region 个人信息
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult Info_Get()
        {
            int id = MemberManager.GetMember().id;
            account_member member = entity.account_member.FirstOrDefault(p => p.id == id);
            return Json(member, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Info_Add_Edit(account_member memberModel)
        {
            var query = entity.account_member.FirstOrDefault(p => p.id == memberModel.id);
            query.real_name = memberModel.real_name;
            query.sex = memberModel.sex;
            query.idcard_number = memberModel.idcard_number;
            query.email = memberModel.email;
            query.remark = memberModel.remark;
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 密码管理
        public ActionResult Password()
        {
            return View();
        }
        public ActionResult Password_Add_Edit(string password)
        {
            int id = MemberManager.GetMember().id;
            account_member member = entity.account_member.FirstOrDefault(p => p.id == id);
            member.password = DESTool.Encrypt(password);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
    }
}
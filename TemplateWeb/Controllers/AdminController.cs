using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Models.DB;

namespace TemplateWeb.Controllers
{
    public class AdminController : Controller
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
        #region 单页管理
        public ActionResult PageList()
        {
            return View();
        }
        public ActionResult PageAdd()
        {
            return View();
        }
        public ActionResult PageList_Get()
        {
            var query = entity.page.OrderByDescending(p => p.id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Page_Get(int id)
        {
            var query = entity.page.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public ActionResult Page_Add_Edit(int id, string title, string content)
        {
            if (id == 0)
            {
                page page = new page()
                {
                    title = title,
                    content = content,
                    sys_datetime = DateTime.Now
                };
                entity.page.Add(page);
            }
            else
            {
                var query = entity.page.FirstOrDefault(p => p.id == id);
                query.title = title;
                query.content = content;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Page_Delete(int id)
        {
            var query = entity.page.FirstOrDefault(p => p.id == id);
            entity.page.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult NavList()
        {
            return View();
        }
        public ActionResult NavAdd()
        {
            return View();
        }
        public ActionResult BannerList()
        {
            return View();
        }
        public ActionResult BannerAdd()
        {
            return View();
        }
    }
}
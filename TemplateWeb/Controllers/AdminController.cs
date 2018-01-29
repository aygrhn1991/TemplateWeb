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
        public ActionResult Test()
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
        #region 导航管理
        public ActionResult NavList()
        {
            return View();
        }
        public ActionResult NavList_Get()
        {
            var query = entity.nav_nav.OrderByDescending(p => p.id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Nav_Add_Edit(int id, string title, bool? enable, bool? has_sub_nav, string url, int? pageid, int? sort)
        {
            if (id == 0)
            {
                var query = entity.nav_nav;
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                nav_nav nav = new nav_nav()
                {
                    title = title,
                    enable = enable,
                    has_sub_nav = has_sub_nav,
                    url = url,
                    pageid = pageid,
                    sort = ++maxSort,
                    sys_datetime = DateTime.Now
                };
                entity.nav_nav.Add(nav);
            }
            else
            {
                var query = entity.nav_nav.FirstOrDefault(p => p.id == id);
                query.title = title;
                query.enable = enable;
                query.has_sub_nav = has_sub_nav;
                query.url = url;
                query.pageid = pageid;
                query.sort = sort;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Nav_Delete(int id)
        {
            var query = entity.nav_nav.FirstOrDefault(p => p.id == id);
            entity.nav_nav.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
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
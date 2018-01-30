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
        #region 导航管理
        public ActionResult NavList()
        {
            return View();
        }
        public ActionResult NavList_Get()
        {
            var query = entity.nav_nav.OrderBy(p => p.sort);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Nav_Add_Edit(int id, string title, bool? enable, int? mode, string url, int? page_id, int? sort)
        {
            if (id == 0)
            {
                var query = entity.nav_nav;
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                nav_nav nav = new nav_nav()
                {
                    title = title,
                    enable = enable,
                    mode = mode,
                    url = url,
                    page_id = page_id,
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
                query.mode = mode;
                query.url = url;
                query.page_id = page_id;
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
        public ActionResult Nav_Sort(int id, string sortType)
        {
            var query = entity.nav_nav.OrderBy(p => p.sort).ToArray();
            for (int i = 0; i < query.Count(); i++)
            {
                if (query[i].id == id)
                {
                    if (sortType == "up")
                    {
                        if (i == 0)
                        {
                            break;
                        }
                        else
                        {
                            int tempSort = query[i].sort.Value;
                            query[i].sort = query[i - 1].sort;
                            query[i - 1].sort = tempSort;
                        }
                    }
                    else
                    {
                        if (i == query.Count() - 1)
                        {
                            break;
                        }
                        else
                        {
                            int tempSort = query[i].sort.Value;
                            query[i].sort = query[i + 1].sort;
                            query[i + 1].sort = tempSort;
                        }
                    }
                }
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 导航内容管理
        public ActionResult NavContent(int id)
        {
            return View();
        }
        public ActionResult Nav_Get(int id)
        {
            var query = entity.nav_nav.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 子导航管理
        public ActionResult SubnavList_Get()
        {
            var query = entity.nva_subnav.OrderBy(p => p.sort);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Subnav_Add_Edit(int id, int nav_id, string title, bool? enable, int? mode, string url, int? page_id, int? sort)
        {
            if (id == 0)
            {
                var query = entity.nva_subnav;
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                nva_subnav subnav = new nva_subnav()
                {
                    nav_id = nav_id,
                    title = title,
                    enable = enable,
                    mode = mode,
                    url = url,
                    page_id = page_id,
                    sort = ++maxSort,
                    sys_datetime = DateTime.Now
                };
                entity.nva_subnav.Add(subnav);
            }
            else
            {
                var query = entity.nav_nav.FirstOrDefault(p => p.id == id);
                query.title = title;
                query.enable = enable;
                query.mode = mode;
                query.url = url;
                query.page_id = page_id;
                query.sort = sort;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
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
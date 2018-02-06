﻿using System;
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
        public ActionResult Page_Add_Edit(page pageModel)
        {
            if (pageModel.id == 0)
            {
                page page = new page()
                {
                    title = pageModel.title,
                    content = pageModel.content,
                    sys_datetime = DateTime.Now
                };
                entity.page.Add(page);
            }
            else
            {
                var query = entity.page.FirstOrDefault(p => p.id == pageModel.id);
                query.title = pageModel.title;
                query.content = pageModel.content;
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
        public ActionResult Nav_Add_Edit(nav_nav navModel)
        {
            if (navModel.id == 0)
            {
                var query = entity.nav_nav;
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                nav_nav nav = new nav_nav()
                {
                    title = navModel.title,
                    enable = navModel.enable,
                    mode = navModel.mode,
                    sort = ++maxSort,
                    page_id = navModel.page_id,
                    url = navModel.url,
                    sys_datetime = DateTime.Now
                };
                entity.nav_nav.Add(nav);
            }
            else
            {
                var query = entity.nav_nav.FirstOrDefault(p => p.id == navModel.id);
                query.title = navModel.title;
                query.enable = navModel.enable;
                query.mode = navModel.mode;
                query.sort = navModel.sort;
                query.page_id = navModel.page_id;
                query.url = navModel.url;
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
        public ActionResult SubnavList_Get(int id)
        {
            var query = entity.nav_subnav.Where(p => p.nav_id == id).OrderBy(p => p.sort);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Subnav_Add_Edit(nav_subnav subnavModel)
        {
            if (subnavModel.id == 0)
            {
                var query = entity.nav_subnav.Where(p => p.nav_id == subnavModel.nav_id);
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                nav_subnav subnav = new nav_subnav()
                {
                    nav_id = subnavModel.nav_id,
                    title = subnavModel.title,
                    enable = subnavModel.enable,
                    mode = subnavModel.mode,
                    sort = ++maxSort,
                    page_id = subnavModel.page_id,
                    url = subnavModel.url,
                    sys_datetime = DateTime.Now
                };
                entity.nav_subnav.Add(subnav);
            }
            else
            {
                var query = entity.nav_subnav.FirstOrDefault(p => p.id == subnavModel.id);
                query.nav_id = subnavModel.nav_id;
                query.title = subnavModel.title;
                query.enable = subnavModel.enable;
                query.mode = subnavModel.mode;
                query.sort = subnavModel.sort;
                query.page_id = subnavModel.page_id;
                query.url = subnavModel.url;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Subnav_Delete(int id)
        {
            var query = entity.nav_subnav.FirstOrDefault(p => p.id == id);
            entity.nav_subnav.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Subnav_Sort(int id, string sortType)
        {
            var query = entity.nav_subnav.OrderBy(p => p.sort).ToArray();
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
        #region 子导航内容管理
        public ActionResult NavsubContent(int id)
        {
            return View();
        }
        public ActionResult Subnav_Get(int id)
        {
            var query = entity.nav_subnav.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 轮播管理
        #region 轮播管理
        public ActionResult BannerList()
        {
            return View();
        }
        public ActionResult BannerAdd()
        {
            return View();
        }
        public ActionResult BannerList_Get()
        {
            var query = entity.banner.OrderBy(p => p.sort);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Banner_Add_Edit(banner bannerModel)
        {
            if (bannerModel.id == 0)
            {
                var query = entity.banner;
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                banner banner = new banner()
                {
                    title = bannerModel.title,
                    enable = bannerModel.enable,
                    mode = bannerModel.mode,
                    sort = ++maxSort,
                    page_id = bannerModel.page_id,
                    url = bannerModel.url,
                    path = bannerModel.path,
                    sys_datetime = DateTime.Now
                };
                entity.banner.Add(banner);
            }
            else
            {
                var query = entity.banner.FirstOrDefault(p => p.id == bannerModel.id);
                query.title = bannerModel.title;
                query.enable = bannerModel.enable;
                query.mode = bannerModel.mode;
                query.sort = bannerModel.sort;
                query.page_id = bannerModel.page_id;
                query.url = bannerModel.url;
                query.path = bannerModel.path;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Banner_Delete(int id)
        {
            var query = entity.banner.FirstOrDefault(p => p.id == id);
            entity.banner.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Banner_Sort(int id, string sortType)
        {
            var query = entity.banner.OrderBy(p => p.sort).ToArray();
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
        public ActionResult BannerContent(int id)
        {
            return View();
        }
        public ActionResult Banner_Get(int id)
        {
            var query = entity.banner.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
    }
}
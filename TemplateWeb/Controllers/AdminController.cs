using System;
using System.Collections.Generic;
using System.IO;
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
        #region 轮播内容管理
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
        #region 合作管理
        public ActionResult PartnerList()
        {
            return View();
        }
        public ActionResult PartnerAdd()
        {
            return View();
        }
        public ActionResult PartnerList_Get()
        {
            var query = entity.partner.OrderBy(p => p.sort);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Partner_Add_Edit(partner partnerModel)
        {
            if (partnerModel.id == 0)
            {
                var query = entity.partner;
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                partner partner = new partner()
                {
                    title = partnerModel.title,
                    enable = partnerModel.enable,
                    sort = ++maxSort,
                    url = partnerModel.url,
                    path = partnerModel.path,
                    sys_datetime = DateTime.Now
                };
                entity.partner.Add(partner);
            }
            else
            {
                var query = entity.partner.FirstOrDefault(p => p.id == partnerModel.id);
                query.title = partnerModel.title;
                query.enable = partnerModel.enable;
                query.sort = partnerModel.sort;
                query.url = partnerModel.url;
                query.path = partnerModel.path;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Partner_Delete(int id)
        {
            var query = entity.partner.FirstOrDefault(p => p.id == id);
            entity.partner.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Partner_Sort(int id, string sortType)
        {
            var query = entity.partner.OrderBy(p => p.sort).ToArray();
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
        #region 链接管理
        #region 链接管理
        public ActionResult LinkList()
        {
            return View();
        }
        public ActionResult LinkList_Get()
        {
            var query = entity.link_link.OrderBy(p => p.sort);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Link_Add_Edit(link_link linkModel)
        {
            if (linkModel.id == 0)
            {
                var query = entity.link_link;
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                link_link link = new link_link()
                {
                    title = linkModel.title,
                    enable = linkModel.enable,
                    mode = linkModel.mode,
                    sort = ++maxSort,
                    page_id = linkModel.page_id,
                    url = linkModel.url,
                    sys_datetime = DateTime.Now
                };
                entity.link_link.Add(link);
            }
            else
            {
                var query = entity.link_link.FirstOrDefault(p => p.id == linkModel.id);
                query.title = linkModel.title;
                query.enable = linkModel.enable;
                query.mode = linkModel.mode;
                query.sort = linkModel.sort;
                query.page_id = linkModel.page_id;
                query.url = linkModel.url;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Link_Delete(int id)
        {
            var query = entity.link_link.FirstOrDefault(p => p.id == id);
            entity.link_link.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Link_Sort(int id, string sortType)
        {
            var query = entity.link_link.OrderBy(p => p.sort).ToArray();
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
        #region 链接内容管理
        public ActionResult LinkContent(int id)
        {
            return View();
        }
        public ActionResult Link_Get(int id)
        {
            var query = entity.link_link.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 子链接管理
        public ActionResult SublinkList_Get(int id)
        {
            var query = entity.link_sublink.Where(p => p.link_id == id).OrderBy(p => p.sort);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sublink_Add_Edit(link_sublink sublinkModel)
        {
            if (sublinkModel.id == 0)
            {
                var query = entity.link_sublink.Where(p => p.link_id == sublinkModel.link_id);
                int maxSort = query.Count() <= 0 ? 0 : query.Max(p => p.sort.Value);
                link_sublink sublink = new link_sublink()
                {
                    link_id = sublinkModel.link_id,
                    title = sublinkModel.title,
                    enable = sublinkModel.enable,
                    mode = sublinkModel.mode,
                    sort = ++maxSort,
                    page_id = sublinkModel.page_id,
                    url = sublinkModel.url,
                    sys_datetime = DateTime.Now
                };
                entity.link_sublink.Add(sublink);
            }
            else
            {
                var query = entity.link_sublink.FirstOrDefault(p => p.id == sublinkModel.id);
                query.link_id = sublinkModel.link_id;
                query.title = sublinkModel.title;
                query.enable = sublinkModel.enable;
                query.mode = sublinkModel.mode;
                query.sort = sublinkModel.sort;
                query.page_id = sublinkModel.page_id;
                query.url = sublinkModel.url;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sublink_Delete(int id)
        {
            var query = entity.link_sublink.FirstOrDefault(p => p.id == id);
            entity.link_sublink.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sublink_Sort(int id, string sortType)
        {
            var query = entity.link_sublink.OrderBy(p => p.sort).ToArray();
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
        #region 子链接内容管理
        public ActionResult LinksubContent(int id)
        {
            return View();
        }
        public ActionResult Sublink_Get(int id)
        {
            var query = entity.link_sublink.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 网站设置
        public ActionResult Setting()
        {
            return View();
        }
        public ActionResult Setting_Get()
        {
            var query = entity.setting.ToArray();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var item in query)
            {
                dictionary.Add(item.key, item.value);
            }
            return Json(dictionary, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Setting_Upload(string key, HttpPostedFileBase file)
        {
            string relativePath = "/Upload/setting/";
            string AabsolutePath = Server.MapPath(relativePath);
            string filename = String.Format(key + "-{0}-{1}-{2}-{3}-{4}-{5}-{6}",
                DateTime.Now.Year,
                DateTime.Now.Month.ToString("D2"),
                DateTime.Now.Day.ToString("D2"),
                DateTime.Now.Hour.ToString("D2"),
                DateTime.Now.Minute.ToString("D2"),
                DateTime.Now.Second.ToString("D2"),
                Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName));
            string imgUrl = relativePath + filename;
            if (!Directory.Exists(Path.GetDirectoryName(AabsolutePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(AabsolutePath));
            }
            file.SaveAs(AabsolutePath + filename);
            var query = entity.setting.FirstOrDefault(p => p.key == key);
            if (query == null)
            {
                query = new setting() { key = key, value = null };
                entity.setting.Add(query);
            }
            query.value = imgUrl;
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Setting_Save(string key, string value)
        {
            var query = entity.setting.FirstOrDefault(p => p.key == key);
            if (query == null)
            {
                query = new setting() { key = key, value = null };
                entity.setting.Add(query);
            }
            query.value = value;
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 留言管理
        public ActionResult MessageBoardList()
        {
            return View();
        }
        public ActionResult MessageBoardList_Get()
        {
            var query = entity.messageaboard.OrderByDescending(p => p.id).ToArray().Select(p => new
            {
                p.id,
                p.contact_name,
                p.contact_phone,
                p.contact_other,
                p.content,
                p.state_mark,
                p.state_read,
                p.state_solve,
                sys_datetime = p.sys_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MessageBoard_Add_Edit(messageaboard messageaboardModel)
        {
            var query = entity.messageaboard.FirstOrDefault(p => p.id == messageaboardModel.id);
            query.state_mark = messageaboardModel.state_mark;
            query.state_read = messageaboardModel.state_read;
            query.state_solve = messageaboardModel.state_solve;
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MessageBoard_Delete(int id)
        {
            var query = entity.messageaboard.FirstOrDefault(p => p.id == id);
            entity.messageaboard.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 招聘管理
        #region 分类管理
        public ActionResult EmployTypeList()
        {
            return View();
        }
        public ActionResult EmployTypeList_Get()
        {
            var query = entity.employ_type.OrderByDescending(p => p.id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployType_Add_Edit(employ_type typeModel)
        {
            if (typeModel.id == 0)
            {
                employ_type type = new employ_type() { name = typeModel.name };
                entity.employ_type.Add(type);
            }
            else
            {
                var query = entity.employ_type.FirstOrDefault(p => p.id == typeModel.id);
                query.name = typeModel.name;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployType_Delete(int id)
        {
            var query = entity.employ_type.FirstOrDefault(p => p.id == id);
            entity.employ_type.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 招聘管理
        public ActionResult EmployList()
        {
            return View();
        }
        public ActionResult EmployAdd()
        {
            return View();
        }
        public ActionResult Employ_Get(int id)
        {
            var query = entity.employ.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployList_Get()
        {
            var query = entity.employ.OrderByDescending(p => p.id).Join(entity.employ_type, a => a.type_id, b => b.id, (a, b) => new
            {
                a.benefit,
                a.education,
                a.employ_number,
                a.experience,
                a.id,
                a.position_description_1,
                a.position_description_2,
                a.position_description_3,
                a.position_description_4,
                a.position_name,
                a.position_requirement_1,
                a.position_requirement_2,
                a.position_requirement_3,
                a.position_requirement_4,
                a.remark,
                a.salary,
                a.sys_datetime,
                a.type_id,
                a.work_place,
                type = b.name
            });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Employ_Add_Edit(employ employModel)
        {
            if (employModel.id == 0)
            {
                employ employ = new employ()
                {
                    type_id = employModel.type_id,
                    position_name = employModel.position_name,
                    salary = employModel.salary,
                    education = employModel.education,
                    experience = employModel.experience,
                    work_place = employModel.work_place,
                    employ_number = employModel.employ_number,
                    position_description_1 = employModel.position_description_1,
                    position_description_2 = employModel.position_description_2,
                    position_description_3 = employModel.position_description_3,
                    position_description_4 = employModel.position_description_4,
                    position_requirement_1 = employModel.position_requirement_1,
                    position_requirement_2 = employModel.position_requirement_2,
                    position_requirement_3 = employModel.position_requirement_3,
                    position_requirement_4 = employModel.position_requirement_4,
                    benefit = employModel.benefit,
                    remark = employModel.remark,
                    sys_datetime = DateTime.Now,
                };
                entity.employ.Add(employ);
            }
            else
            {
                var query = entity.employ.FirstOrDefault(p => p.id == employModel.id);
                query.type_id = employModel.type_id;
                query.position_name = employModel.position_name;
                query.salary = employModel.salary;
                query.education = employModel.education;
                query.experience = employModel.experience;
                query.work_place = employModel.work_place;
                query.employ_number = employModel.employ_number;
                query.position_description_1 = employModel.position_description_1;
                query.position_description_2 = employModel.position_description_2;
                query.position_description_3 = employModel.position_description_3;
                query.position_description_4 = employModel.position_description_4;
                query.position_requirement_1 = employModel.position_requirement_1;
                query.position_requirement_2 = employModel.position_requirement_2;
                query.position_requirement_3 = employModel.position_requirement_3;
                query.position_requirement_4 = employModel.position_requirement_4;
                query.benefit = employModel.benefit;
                query.remark = employModel.remark;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Employ_Delete(int id)
        {
            var query = entity.employ.FirstOrDefault(p => p.id == id);
            entity.employ.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 新闻管理
        #region 分类管理
        public ActionResult NewsTypeList()
        {
            return View();
        }
        public ActionResult NewsTypeList_Get()
        {
            var query = entity.news_type.OrderByDescending(p => p.id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewsType_Add_Edit(news_type typeModel)
        {
            if (typeModel.id == 0)
            {
                news_type type = new news_type() { name = typeModel.name };
                entity.news_type.Add(type);
            }
            else
            {
                var query = entity.news_type.FirstOrDefault(p => p.id == typeModel.id);
                query.name = typeModel.name;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewsType_Delete(int id)
        {
            var query = entity.news_type.FirstOrDefault(p => p.id == id);
            entity.news_type.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 新闻管理
        public ActionResult NewsList()
        {
            return View();
        }
        public ActionResult NewsAdd()
        {
            return View();
        }
        public ActionResult News_Get(int id)
        {
            var query = entity.news.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewsList_Get()
        {
            var query = entity.news.OrderByDescending(p => p.id).Join(entity.news_type, a => a.type_id, b => b.id, (a, b) => new
            {
                a.author,
                a.content,
                datetime = a.datetime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                a.description,
                a.id,
                a.path,
                a.sys_datetime,
                a.title,
                a.top,
                a.type_id,
                a.views,
                type = b.name,
            });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult News_Add_Edit(news newsModel)
        {
            if (newsModel.id == 0)
            {
                news news = new news()
                {
                    id = 0,
                    author = newsModel.author,
                    content = newsModel.content,
                    datetime = newsModel.datetime,
                    description = newsModel.description,
                    path = newsModel.path,
                    title = newsModel.title,
                    top = newsModel.top,
                    type_id = newsModel.type_id,
                    views = newsModel.views,
                    sys_datetime = DateTime.Now,
                };
                entity.news.Add(news);
            }
            else
            {
                var query = entity.news.FirstOrDefault(p => p.id == newsModel.id);
                query.author = newsModel.author;
                query.content = newsModel.content;
                query.datetime = newsModel.datetime;
                query.description = newsModel.description;
                query.path = newsModel.path;
                query.title = newsModel.title;
                query.top = newsModel.top;
                query.type_id = newsModel.type_id;
                query.views = newsModel.views;
            }
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult News_Delete(int id)
        {
            var query = entity.news.FirstOrDefault(p => p.id == id);
            entity.news.Remove(query);
            return Json(entity.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
    }
}
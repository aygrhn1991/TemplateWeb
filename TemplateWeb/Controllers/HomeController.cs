using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Models.DB;

namespace TemplateWeb.Controllers
{
    public class HomeController : Controller
    {
        EntityDB entity = new EntityDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexParam_Get()
        {
            var nav = entity.lay_nav_nav.Where(p => p.enable == true).OrderBy(p => p.sort).ToArray().Select(p => new
            {
                p.id,
                p.title,
                p.mode,
                p.page_id,
                p.url,
                subnav = entity.lay_nav_subnav.Where(q => q.nav_id == p.id && q.enable == true).OrderBy(q => q.sort).Select(q => new
                {
                    q.id,
                    q.title,
                    q.mode,
                    q.page_id,
                    q.url,
                }),
            });
            var notice = entity.lay_notice.Where(p => p.enable == true).OrderBy(p => p.sort).Select(p => new
            {
                p.id,
                p.content,
            });
            var banner = entity.lay_banner.Where(p => p.enable == true).OrderBy(p => p.sort).Select(p => new
            {
                p.id,
                p.title,
                p.path,
                p.mode,
                p.page_id,
                p.url,
            });
            var partner = entity.lay_partner.Where(p => p.enable == true).OrderBy(p => p.sort).Select(p => new
            {
                p.id,
                p.title,
                p.path,
                p.url,
            });
            var link = entity.lay_link_link.Where(p => p.enable == true).OrderBy(p => p.sort).ToArray().Select(p => new
            {
                p.id,
                p.title,
                p.mode,
                p.page_id,
                p.url,
                subnav = entity.lay_link_sublink.Where(q => q.link_id == p.id && q.enable == true).OrderBy(q => q.sort).Select(q => new
                {
                    q.id,
                    q.title,
                    q.mode,
                    q.page_id,
                    q.url,
                }),
            });
            var paramList = entity.lay_setting.ToArray();
            Dictionary<string, string> param = new Dictionary<string, string>();
            foreach (var item in paramList)
            {
                param.Add(item.key, item.value);
            }
            return Json(new
            {
                nav,
                notice,
                banner,
                partner,
                link,
                param,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexContent_Get()
        {
            var product = entity.module_product_type.OrderByDescending(p => p.id).ToArray().Select(p => new
            {
                p.id,
                p.name,
                product = entity.module_product.Where(q => q.type_id == p.id).OrderByDescending(q => q.top).ThenBy(q => q.id).Take(4).Select(q => new
                {
                    q.id,
                    q.type_id,
                    q.name,
                    q.path,
                    q.description,
                    q.top,
                }),
            });
            var news = entity.module_news_type.OrderByDescending(p => p.id).ToArray().Select(p => new
            {
                p.id,
                p.name,
                news = entity.module_news.Where(q => q.type_id == p.id).OrderByDescending(q => q.top).ThenBy(q => q.id).Take(4).ToArray().Select(q => new
                {
                    q.id,
                    q.type_id,
                    q.title,
                    q.author,
                    datetime = q.datetime.Value.ToString("yyyy-MM-dd"),
                    q.path,
                    q.description,
                    q.top,
                    q.views,
                }),
            });
            return Json(new
            {
                product,
                news,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Component;
using TemplateWeb.Models;
using TemplateWeb.Models.DB;

namespace TemplateWeb.Controllers
{
    public class HomeController : Controller
    {
        EntityDB entity = new EntityDB();
        public ActionResult Index()
        {
            //if (Request.Browser.IsMobileDevice)
            //{
            //    return RedirectToAction("Index", "Mobile");
            //}
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Layout_Get()
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
            var member = MemberManager.GetMember();
            var member_messageCount = 0;
            if (member != null)
            {
                member_messageCount = entity.member_message.Count(p => p.member_id == member.id && p.state_read == false);
            }
            return Json(new
            {
                nav,
                notice,
                banner,
                partner,
                link,
                param,
                member,
                member_messageCount,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index_Get()
        {
            var product = entity.module_product_type.OrderByDescending(p => p.id).ToArray().Select(p => new
            {
                p.id,
                p.name,
                product = entity.module_product.Where(q => q.type_id == p.id && q.delete == false).OrderByDescending(q => q.top).ThenBy(q => q.id).Take(4).Select(q => new
                {
                    q.id,
                    q.type_id,
                    q.name,
                    q.path,
                    q.description,
                    q.top,
                    q.price,
                    q.delete,
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

        #region 单页
        public ActionResult Page()
        {
            return View();
        }
        public ActionResult Page_Get(int id)
        {
            lay_page page = entity.lay_page.FirstOrDefault(p => p.id == id);
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 新闻
        public ActionResult NewsList()
        {
            return View();
        }
        public ActionResult NewsTypeList_Get()
        {
            var query = entity.module_news_type;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewsList_Get(int id)
        {
            var query = entity.module_news.Where(p => p.type_id == id).OrderByDescending(p => p.top).ThenBy(p => p.id).ToArray().Select(q => new
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
            });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewsDetail()
        {
            return View();
        }
        public ActionResult NewsDetail_Get(int id)
        {
            var query = entity.module_news.FirstOrDefault(p => p.id == id);
            query.views++;
            entity.SaveChanges();
            return Json(new
            {
                query.id,
                query.type_id,
                query.title,
                query.author,
                datetime = query.datetime.Value.ToString("yyyy-MM-dd"),
                query.path,
                query.description,
                query.top,
                query.views,
                query.content,
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 产品
        public ActionResult ProductList()
        {
            return View();
        }
        public ActionResult ProductTypeList_Get()
        {
            var query = entity.module_product_type;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductList_Get(int id)
        {
            var query = entity.module_product.Where(p => p.type_id == id && p.delete == false).OrderByDescending(p => p.top).ThenBy(p => p.id).Select(q => new
            {
                q.id,
                q.type_id,
                q.name,
                q.path,
                q.description,
                q.top,
                q.price,
                q.delete,
            });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductDetail()
        {
            return View();
        }
        public ActionResult ProductDetail_Get(int id)
        {
            var query = entity.module_product.FirstOrDefault(p => p.id == id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult jssdkconfig(string url)
        {
            //if (Request.Browser.IsMobileDevice)
            //{
            //    return RedirectToAction("Index", "Mobile");
            //}
            return Json(GetJSSDKConfig(url), JsonRequestBehavior.AllowGet);
        }
        #region JS-SDK
        public JSSDKConfig GetJSSDKConfig(string url)
        {
            JSSDKConfig jssdkconfig = new JSSDKConfig();
            jssdkconfig.appId = "wx6f45b83b6fe25fad";
            jssdkconfig.nonceStr = GetNonceStr();
            jssdkconfig.timestamp = GetTimeStamp(DateTime.Now).ToString();
            jssdkconfig.signature = GetJSSDKSignature(jssdkconfig.nonceStr, jssdkconfig.timestamp, url);
            return jssdkconfig;
        }
        private string GetJSSDKSignature(string nonceStr, string timeStamp, string url)
        {
            string ticket = RequestJSApiTicket();
            string[] array = { "noncestr=" + nonceStr, "jsapi_ticket=" + ticket, "timestamp=" + timeStamp, "url=" + url };
            Array.Sort(array);
            string str = string.Join("&", array);
            SHA1 sha1 = SHA1.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            bytes = sha1.ComputeHash(bytes);
            sha1.Clear();
            string signature = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return signature;
        }
        private string RequestJSApiTicket()
        {
            string accesstoken = RequestAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + accesstoken + "&type=jsapi";
            string response = HttpHelper(url, "GET");
            JSApiTicket ticket = (JSApiTicket)JsonConvert.DeserializeObject<JSApiTicket>(response);
            return ticket.ticket;
        }
        private string GetNonceStr(int length = 30)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string nonceStr = "";
            for (int i = 0; i < length; i++)
            {
                nonceStr += chars.Substring(random.Next(0, chars.Length - 1), 1);
            }
            return nonceStr;
        }
        private int GetTimeStamp(DateTime time)
        {
            return (int)((time - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
        }
        private string RequestAccessToken()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx6f45b83b6fe25fad&secret=d7fce1e61737ce7d5746d7ae0e742d85";
            string response = HttpHelper(url, "GET");
            AccessToken accessToken = (AccessToken)JsonConvert.DeserializeObject<AccessToken>(response);
            return accessToken.access_token;
        }
        private string HttpHelper(string url, string requestMethod)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = requestMethod.ToString();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    string responseString = streamReader.ReadToEnd();
                    return responseString;
                }
            }
        }
        #endregion
    }
}
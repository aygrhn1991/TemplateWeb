using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateWeb.Extension
{
    public class AdminAuthorizeAttribute: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies["tpadmin"];
            if (cookie == null)
            {
                return false;
            }
            else
            {
                string cookie_str = SecurityDES.Decrypt(cookie.Value);
                try
                {
                    AdminCookieModel model = (AdminCookieModel)JsonConvert.DeserializeObject(cookie_str, typeof(AdminCookieModel));
                    if (model.isauth == true)
                    {
                        if (Roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length == 0)
                        {
                            return true;
                        }
                        else
                        {
                            foreach (var item in Roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (model.roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Contains(item))
                                {
                                    return true;
                                }
                            }
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["jjsadmin"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Login");
            }
            else
            {
                filterContext.Result = new RedirectResult("/Home/Error?error=" + "您所在的用户组权限不足！");
            }
        }
    }
}
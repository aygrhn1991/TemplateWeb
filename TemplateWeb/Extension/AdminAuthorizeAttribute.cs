using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Models.Account;

namespace TemplateWeb.Extension
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            AdminModel model = AdminManager.GetAdmin();
            if (model == null)
            {
                return false;
            }
            else
            {
                if (model.isAuth == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Admin/Login");
        }
    }
}
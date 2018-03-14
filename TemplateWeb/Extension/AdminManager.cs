using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateWeb.Models.Account;
using TemplateWeb.Models.DB;

namespace TemplateWeb.Extension
{
    public class AdminManager
    {
        EntityDB entity = new EntityDB();
        public static AdminModel GetAdmin()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["tpadmin"];
            if (cookie == null)
            {
                return null;
            }
            string cookieStr = DESTool.Decrypt(cookie.Value);
            AdminModel model = (AdminModel)JsonConvert.DeserializeObject(cookieStr, typeof(AdminModel));
            return model;
        }
    }
}
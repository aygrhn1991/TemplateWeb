﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateWeb.Models.Account;
using TemplateWeb.Models.DB;

namespace TemplateWeb.Extension
{
    public class MemberManager
    {
        EntityDB entity = new EntityDB();
        public static account_member GetMember()
        {
            var session = HttpContext.Current.Session["tpmember"];
            if (session == null)
            {
                return null;
            }
            return (account_member)session;
        }
    }
}
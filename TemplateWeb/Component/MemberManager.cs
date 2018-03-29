using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateWeb.Models.DB;

namespace TemplateWeb.Component
{
    public class MemberManager
    {

        public static account_member GetMember()
        {
            var session = HttpContext.Current.Session["tpmember"];
            if (session == null)
            {
                return null;
            }
            return (account_member)session;
        }
        public static bool CreateMember(string phone, string password)
        {
            EntityDB entity = new EntityDB();
            account_member member = new account_member()
            {
                enable = true,
                password = DESTool.Encrypt(password),
                phone = phone,
                sys_datetime = DateTime.Now,
            };
            entity.account_member.Add(member);
            return entity.SaveChanges() > 0;
        }
    }
}
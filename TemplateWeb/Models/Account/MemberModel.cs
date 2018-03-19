using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateWeb.Models.Account
{
    public class MemberModel
    {
        public string phone { get; set; }
        public string password { get; set; }
        public bool isAuth { get; set; }
    }
}
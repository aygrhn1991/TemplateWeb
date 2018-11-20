using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateWeb.Models
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public DateTime get_time { get; set; }
    }
}
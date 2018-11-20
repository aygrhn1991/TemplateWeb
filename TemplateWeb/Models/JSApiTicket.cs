using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateWeb.Models
{
    public class JSApiTicket
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }
        public DateTime get_time { get; set; }
    }
}
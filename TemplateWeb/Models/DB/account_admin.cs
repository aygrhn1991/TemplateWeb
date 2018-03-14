namespace TemplateWeb.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class account_admin
    {
        public int id { get; set; }

        public string phone { get; set; }

        public string password { get; set; }

        public string admin_name { get; set; }

        public bool? enable { get; set; }

        public DateTime? sys_datetime { get; set; }
    }
}

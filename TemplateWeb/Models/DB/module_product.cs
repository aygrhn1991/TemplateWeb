namespace TemplateWeb.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class module_product
    {
        public int id { get; set; }

        public int? type_id { get; set; }

        public string name { get; set; }

        public string path { get; set; }

        public string description { get; set; }

        public string content { get; set; }

        public bool? top { get; set; }

        public DateTime? sys_datetime { get; set; }
    }
}

namespace TemplateWeb.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("banner")]
    public partial class banner
    {
        public int id { get; set; }

        public string title { get; set; }

        public string path { get; set; }

        public int? page_id { get; set; }

        public string url { get; set; }

        public bool? enable { get; set; }

        public int? mode { get; set; }

        public int? sort { get; set; }

        public DateTime? sys_datetime { get; set; }
    }
}

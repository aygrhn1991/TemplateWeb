namespace TemplateWeb.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("partner")]
    public partial class partner
    {
        public int id { get; set; }

        public string title { get; set; }

        public bool? enable { get; set; }

        public int? sort { get; set; }

        public string url { get; set; }

        public string path { get; set; }

        public DateTime? sys_datetime { get; set; }
    }
}

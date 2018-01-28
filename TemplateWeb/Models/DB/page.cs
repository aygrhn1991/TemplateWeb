namespace TemplateWeb.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("page")]
    public partial class page
    {
        public int id { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public DateTime? sys_datetime { get; set; }
    }
}

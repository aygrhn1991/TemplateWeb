namespace TemplateWeb.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("employ")]
    public partial class employ
    {
        public int id { get; set; }

        public int? type_id { get; set; }

        public string position_name { get; set; }

        public string salary { get; set; }

        public string education { get; set; }

        public string experience { get; set; }

        public string work_place { get; set; }

        public int? employ_number { get; set; }

        public string position_description { get; set; }

        public string position_requirement { get; set; }

        public string benefit { get; set; }

        public DateTime? sys_datetime { get; set; }
    }
}

namespace TemplateWeb.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pay_order
    {
        public int id { get; set; }

        public int? product_id { get; set; }

        public int? member_id { get; set; }

        [Column(TypeName = "money")]
        public decimal? price { get; set; }

        public int? state { get; set; }
        
        public DateTime? pay_time { get; set; }

        public DateTime? sys_datetime { get; set; }
    }
}

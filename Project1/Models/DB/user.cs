namespace Project1.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        public int id { get; set; }

        public string phone { get; set; }

        public string name { get; set; }

        public string school { get; set; }

        public DateTime? sys_datetime { get; set; }
    }
}

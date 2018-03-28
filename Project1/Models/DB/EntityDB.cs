namespace Project1.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityDB : DbContext
    {
        public EntityDB()
            : base("name=EntityDB")
        {
        }

        public virtual DbSet<phonecode> phonecode { get; set; }
        public virtual DbSet<setting> setting { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

namespace TemplateWeb.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityDB : DbContext
    {
        public EntityDB()
            : base("name=EntityDB_Local")
        {
        }

        public virtual DbSet<nav_nav> nav_nav { get; set; }
        public virtual DbSet<nva_subnav> nva_subnav { get; set; }
        public virtual DbSet<page> page { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

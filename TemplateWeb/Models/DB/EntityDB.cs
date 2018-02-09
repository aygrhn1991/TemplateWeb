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

        public virtual DbSet<banner> banner { get; set; }
        public virtual DbSet<employ> employ { get; set; }
        public virtual DbSet<employ_type> employ_type { get; set; }
        public virtual DbSet<link_link> link_link { get; set; }
        public virtual DbSet<link_sublink> link_sublink { get; set; }
        public virtual DbSet<messageaboard> messageaboard { get; set; }
        public virtual DbSet<nav_nav> nav_nav { get; set; }
        public virtual DbSet<nav_subnav> nav_subnav { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<news_type> news_type { get; set; }
        public virtual DbSet<page> page { get; set; }
        public virtual DbSet<partner> partner { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<product_type> product_type { get; set; }
        public virtual DbSet<setting> setting { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

using System.Data.Entity;
using Legalacts.Model.Entities.Mapping;
using Legalacts.Model.Sitemap.Entities;
using Legalacts.Model.Sitemap.Entities.Mapping;

namespace Legalacts.Model.Entities
{
    public partial class SitemapContext : DbContext
    {
        static SitemapContext()
        {
            Database.SetInitializer<SitemapContext>(null);
        }

        public SitemapContext()
            : base("Name=SitemapContext")
        {
        }

        public DbSet<Index> Indexes { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Sitemap.Entities.Sitemap> Sitemaps { get; set; }
        public DbSet<SystemVariable> SystemVariables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IndexMap());
            modelBuilder.Configurations.Add(new RouteMap());
            modelBuilder.Configurations.Add(new SitemapMap());
            modelBuilder.Configurations.Add(new SystemVariableMap());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

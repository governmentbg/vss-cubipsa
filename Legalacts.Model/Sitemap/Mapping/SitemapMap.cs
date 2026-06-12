using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Sitemap.Entities.Mapping
{
    public class SitemapMap : EntityTypeConfiguration<Sitemap>
    {
        public SitemapMap()
        {
            // Primary Key
            this.HasKey(t => t.SitemapId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.XML)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Sitemaps");
            this.Property(t => t.SitemapId).HasColumnName("SitemapId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.XML).HasColumnName("XML");
            this.Property(t => t.IndexID).HasColumnName("IndexID");

            // Relationships
            this.HasRequired(t => t.Index)
                .WithMany(t => t.Sitemaps)
                .HasForeignKey(d => d.IndexID);

        }
    }
}

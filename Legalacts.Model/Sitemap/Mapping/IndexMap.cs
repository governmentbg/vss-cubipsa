using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Sitemap.Entities.Mapping
{
    public class IndexMap : EntityTypeConfiguration<Index>
    {
        public IndexMap()
        {
            // Primary Key
            this.HasKey(t => t.IndexId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.XML)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Indexes");
            this.Property(t => t.IndexId).HasColumnName("IndexId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.XML).HasColumnName("XML");
            this.Property(t => t.RouteId).HasColumnName("RouteId");

            // Relationships
            this.HasRequired(t => t.Route)
                .WithMany(t => t.Indexes)
                .HasForeignKey(d => d.RouteId);

        }
    }
}

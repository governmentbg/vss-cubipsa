using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Sitemap.Entities.Mapping
{
    public class RouteMap : EntityTypeConfiguration<Route>
    {
        public RouteMap()
        {
            // Primary Key
            this.HasKey(t => t.RouteId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Routes");
            this.Property(t => t.RouteId).HasColumnName("RouteId");
            this.Property(t => t.Date).HasColumnName("Date");
        }
    }
}

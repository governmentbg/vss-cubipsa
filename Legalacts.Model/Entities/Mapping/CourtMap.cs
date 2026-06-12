using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class CourtMap : EntityTypeConfiguration<Court>
    {
        public CourtMap()
        {
            // Primary Key
            this.HasKey(t => t.CourtId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Courts");
            this.Property(t => t.CourtId).HasColumnName("CourtId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.EcliCode).HasColumnName("EcliCode");
        }
    }
}

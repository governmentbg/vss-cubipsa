using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class ConnectedTypeMap : EntityTypeConfiguration<ConnectedType>
    {
        public ConnectedTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ConnectedTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ConnectedTypes");
            this.Property(t => t.ConnectedTypeId).HasColumnName("ConnectedTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

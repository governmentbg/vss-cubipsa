using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class ConnectedKindMap : EntityTypeConfiguration<ConnectedKind>
    {
        public ConnectedKindMap()
        {
            // Primary Key
            this.HasKey(t => t.ConnectedKindId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ConnectedKinds");
            this.Property(t => t.ConnectedKindId).HasColumnName("ConnectedKindId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

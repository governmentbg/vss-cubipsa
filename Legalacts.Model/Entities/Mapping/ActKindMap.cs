using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class ActKindMap : EntityTypeConfiguration<ActKind>
    {
        public ActKindMap()
        {
            // Primary Key
            this.HasKey(t => t.ActKindId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ActKinds");
            this.Property(t => t.ActKindId).HasColumnName("ActKindId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class IndocKindMap : EntityTypeConfiguration<IndocKind>
    {
        public IndocKindMap()
        {
            // Primary Key
            this.HasKey(t => t.IndocKindId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("IndocKinds");
            this.Property(t => t.IndocKindId).HasColumnName("IndocKindId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

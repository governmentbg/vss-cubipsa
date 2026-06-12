using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class AppealKindMap : EntityTypeConfiguration<AppealKind>
    {
        public AppealKindMap()
        {
            // Primary Key
            this.HasKey(t => t.AppealKindId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("AppealKinds");
            this.Property(t => t.AppealKindId).HasColumnName("AppealKindId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

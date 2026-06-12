using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class ActionLogTypeMap : EntityTypeConfiguration<ActionLogType>
    {
        public ActionLogTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ActionLogTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ActionLogTypes");
            this.Property(t => t.ActionLogTypeId).HasColumnName("ActionLogTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

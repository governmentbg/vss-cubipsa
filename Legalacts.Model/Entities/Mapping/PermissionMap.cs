using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ResourceName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Permissions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ResourceName).HasColumnName("ResourceName");

            // Relationships
            this.HasMany(t => t.Roles)
                .WithMany(t => t.Permissions)
                .Map(m =>
                    {
                        m.ToTable("RolesPermissions");
                        m.MapLeftKey("PermissionId");
                        m.MapRightKey("RoleId");
                    });


        }
    }
}

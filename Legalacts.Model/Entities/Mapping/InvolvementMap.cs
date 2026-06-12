using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class InvolvementMap : EntityTypeConfiguration<Involvement>
    {
        public InvolvementMap()
        {
            // Primary Key
            this.HasKey(t => t.InvolvementId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Involvements");
            this.Property(t => t.InvolvementId).HasColumnName("InvolvementId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class CaseKindMap : EntityTypeConfiguration<CaseKind>
    {
        public CaseKindMap()
        {
            // Primary Key
            this.HasKey(t => t.CaseKindId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CaseKinds");
            this.Property(t => t.CaseKindId).HasColumnName("CaseKindId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.EcliCode).HasColumnName("EcliCode");
            this.Property(t => t.Abbreviation).HasColumnName("Abbreviation");
        }
    }
}

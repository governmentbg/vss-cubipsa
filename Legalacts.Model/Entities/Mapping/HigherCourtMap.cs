using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class HigherCourtMap : EntityTypeConfiguration<HigherCourt>
    {
        public HigherCourtMap()
        {
            // Primary Key
            this.HasKey(t => t.HigherCourtId);

            // Properties
            // Table & Column Mappings
            this.ToTable("HigherCourts");
            this.Property(t => t.HigherCourtId).HasColumnName("HigherCourtId");
            this.Property(t => t.CourtId).HasColumnName("CourtId");
            this.Property(t => t.OutputNumber).HasColumnName("OutputNumber");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.SendToDocumentKindId).HasColumnName("SendToDocumentKindId");
            this.Property(t => t.DateOfDispatch).HasColumnName("DateOfDispatch");

            // Relationships
            this.HasRequired(t => t.Court)
                .WithMany(t => t.HigherCourts)
                .HasForeignKey(d => d.CourtId);
            this.HasRequired(t => t.SendToDocumentKind)
                .WithMany(t => t.HigherCourts)
                .HasForeignKey(d => d.SendToDocumentKindId);

        }
    }
}

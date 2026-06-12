using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class ActMap : EntityTypeConfiguration<Act>
    {
        public ActMap()
        {
            // Primary Key
            this.HasKey(t => t.ActId);

            // Properties
            this.Property(t => t.Judge)
                .HasMaxLength(200);

            this.Property(t => t.ResultOfAppeal)
                .HasMaxLength(10);

            this.Property(t => t.UID)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("Acts");
            this.Property(t => t.ActId).HasColumnName("ActId");
            this.Property(t => t.ActNumber).HasColumnName("ActNumber");
            this.Property(t => t.CaseNumber).HasColumnName("CaseNumber");
            this.Property(t => t.Judge).HasColumnName("Judge");
            this.Property(t => t.ActKindId).HasColumnName("ActKindId");
            this.Property(t => t.CaseKindId).HasColumnName("CaseKindId");
            this.Property(t => t.CaseYear).HasColumnName("CaseYear");
            this.Property(t => t.ActYear).HasColumnName("ActYear");
            this.Property(t => t.CourtId).HasColumnName("CourtId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.ActDocumentId).HasColumnName("ActDocumentId");
            this.Property(t => t.MotiveDocumentId).HasColumnName("MotiveDocumentId");
            this.Property(t => t.MotiveDate).HasColumnName("MotiveDate");
            this.Property(t => t.LegalDate).HasColumnName("LegalDate");
            this.Property(t => t.HigherCourtId).HasColumnName("HigherCourtId");
            this.Property(t => t.StatusId).HasColumnName("StatusId");
            this.Property(t => t.ResultOfAppeal).HasColumnName("ResultOfAppeal");
            this.Property(t => t.UID).HasColumnName("UID");
            this.Property(t => t.EcliCode).HasColumnName("EcliCode");
            this.Property(t => t.PreviousEcliCode).HasColumnName("PreviousEcliCode");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");

            this.Ignore(t => t.ResultOfAppealDescription);

            // Relationships
            this.HasMany(t => t.ConnectedActs)
                .WithMany(t => t.Acts)
                .Map(m =>
                {
                    m.ToTable("ConnectedActs");
                    m.MapLeftKey("ActId");
                    m.MapRightKey("ConnectedActId");
                });

            this.HasOptional(t => t.ActDocument)
                .WithMany(t => t.Acts)
                .HasForeignKey(d => d.ActDocumentId);
            this.HasRequired(t => t.ActKind)
                .WithMany(t => t.Acts)
                .HasForeignKey(d => d.ActKindId);
            this.HasRequired(t => t.CaseKind)
                .WithMany(t => t.Acts)
                .HasForeignKey(d => d.CaseKindId);
            this.HasRequired(t => t.Court)
                .WithMany(t => t.Acts)
                .HasForeignKey(d => d.CourtId);
            this.HasOptional(t => t.HigherCourt)
                .WithMany(t => t.Acts)
                .HasForeignKey(d => d.HigherCourtId);
            this.HasOptional(t => t.MotiveDocument)
                .WithMany(t => t.Acts)
                .HasForeignKey(d => d.MotiveDocumentId);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.Acts)
                .HasForeignKey(d => d.StatusId);

        }
    }
}

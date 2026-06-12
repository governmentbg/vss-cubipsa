using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class DeletedActMap : EntityTypeConfiguration<DeletedAct>
    {
        public DeletedActMap()
        {
            // Primary Key
            this.HasKey(t => t.DeletedActId);

            // Properties
            this.Property(t => t.Judge)
                .HasMaxLength(200);

            this.Property(t => t.ResultOfAppeal)
                .HasMaxLength(10);

            this.Property(t => t.UID)
                .HasMaxLength(40);

            this.Property(t => t.EcliCode)
                .HasMaxLength(37);

            this.Property(t => t.PreviousEcliCode)
                .HasMaxLength(37);

            // Table & Column Mappings
            this.ToTable("DeletedActs");
            this.Property(t => t.DeletedActId).HasColumnName("DeletedActId");
            this.Property(t => t.ActNumber).HasColumnName("ActNumber");
            this.Property(t => t.CaseNumber).HasColumnName("CaseNumber");
            this.Property(t => t.Judge).HasColumnName("Judge");
            this.Property(t => t.ActKindId).HasColumnName("ActKindId");
            this.Property(t => t.CaseKindId).HasColumnName("CaseKindId");
            this.Property(t => t.CaseYear).HasColumnName("CaseYear");
            this.Property(t => t.ActYear).HasColumnName("ActYear");
            this.Property(t => t.CourtId).HasColumnName("CourtId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.StatusId).HasColumnName("StatusId");
            this.Property(t => t.ResultOfAppeal).HasColumnName("ResultOfAppeal");
            this.Property(t => t.UID).HasColumnName("UID");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.EcliCode).HasColumnName("EcliCode");
            this.Property(t => t.PreviousEcliCode).HasColumnName("PreviousEcliCode");
            this.Property(t => t.IsSynced).HasColumnName("IsSynced");

            // Relationships
            this.HasRequired(t => t.ActKind)
                .WithMany(t => t.DeletedActs)
                .HasForeignKey(d => d.ActKindId);
            this.HasRequired(t => t.CaseKind)
                .WithMany(t => t.DeletedActs)
                .HasForeignKey(d => d.CaseKindId);
            this.HasRequired(t => t.Court)
                .WithMany(t => t.DeletedActs)
                .HasForeignKey(d => d.CourtId);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.DeletedActs)
                .HasForeignKey(d => d.StatusId);

        }
    }
}

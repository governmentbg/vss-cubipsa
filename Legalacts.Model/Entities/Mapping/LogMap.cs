using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogId);

            // Properties
            this.Property(t => t.UID)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("Logs");
            this.Property(t => t.LogId).HasColumnName("LogId");
            this.Property(t => t.ActionLogTypeId).HasColumnName("ActionLogTypeId");
            this.Property(t => t.DatetimeOfAction).HasColumnName("DatetimeOfAction");
            this.Property(t => t.CourtId).HasColumnName("CourtId");
            this.Property(t => t.CaseNumber).HasColumnName("CaseNumber");
            this.Property(t => t.ActKindId).HasColumnName("ActKindId");
            this.Property(t => t.UID).HasColumnName("UID");

            // Relationships
            this.HasRequired(t => t.ActionLogType)
                .WithMany(t => t.Logs)
                .HasForeignKey(d => d.ActionLogTypeId);
            this.HasOptional(t => t.ActKind)
                .WithMany(t => t.Logs)
                .HasForeignKey(d => d.ActKindId);
            this.HasRequired(t => t.Court)
                .WithMany(t => t.Logs)
                .HasForeignKey(d => d.CourtId);

        }
    }
}

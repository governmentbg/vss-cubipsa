using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class ConnectedCasMap : EntityTypeConfiguration<ConnectedCase>
    {
        public ConnectedCasMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ConnectedCaseId, t.ConnectedTypeId, t.ConnectedKindId, t.ActId, t.CourtId, t.AppealActKindId });

            // Properties
            this.Property(t => t.ConnectedCaseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ConnectedTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ConnectedKindId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CourtId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AppealActKindId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ConnectedCases");
            this.Property(t => t.ConnectedCaseId).HasColumnName("ConnectedCaseId");
            this.Property(t => t.ConnectedTypeId).HasColumnName("ConnectedTypeId");
            this.Property(t => t.ConnectedKindId).HasColumnName("ConnectedKindId");
            this.Property(t => t.ActId).HasColumnName("ActId");
            this.Property(t => t.CourtId).HasColumnName("CourtId");
            this.Property(t => t.AppealActKindId).HasColumnName("AppealActKindId");
            this.Property(t => t.CaseNumber).HasColumnName("CaseNumber");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.NumberOfAppealAct).HasColumnName("NumberOfAppealAct");
            this.Property(t => t.DateOfAppealAct).HasColumnName("DateOfAppealAct");

            // Relationships
            this.HasRequired(t => t.ActKind)
                .WithMany(t => t.ConnectedCases)
                .HasForeignKey(d => d.AppealActKindId);
            this.HasRequired(t => t.Act)
                .WithMany(t => t.ConnectedCases)
                .HasForeignKey(d => d.ActId);
            this.HasRequired(t => t.ConnectedKind)
                .WithMany(t => t.ConnectedCases)
                .HasForeignKey(d => d.ConnectedKindId);
            this.HasRequired(t => t.ConnectedType)
                .WithMany(t => t.ConnectedCases)
                .HasForeignKey(d => d.ConnectedTypeId);

        }
    }
}

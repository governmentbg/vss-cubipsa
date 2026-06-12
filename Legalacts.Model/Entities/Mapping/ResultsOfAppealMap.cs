using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class ResultsOfAppealMap : EntityTypeConfiguration<ResultsOfAppeal>
    {
        public ResultsOfAppealMap()
        {
            // Primary Key
            this.HasKey(t => t.ResultsOfAppealId);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ResultsOfAppeals");
            this.Property(t => t.ResultsOfAppealId).HasColumnName("ResultsOfAppealId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

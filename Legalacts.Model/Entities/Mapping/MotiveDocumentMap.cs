using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class MotiveDocumentMap : EntityTypeConfiguration<MotiveDocument>
    {
        public MotiveDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.MotiveDocumentId);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.MimeType)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MotiveDocuments");
            this.Property(t => t.MotiveDocumentId).HasColumnName("MotiveDocumentId");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.MimeType).HasColumnName("MimeType");
        }
    }
}

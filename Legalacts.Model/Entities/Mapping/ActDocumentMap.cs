using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class ActDocumentMap : EntityTypeConfiguration<ActDocument>
    {
        public ActDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ActDocumentId);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.MimeType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Extension)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ActDocuments");
            this.Property(t => t.ActDocumentId).HasColumnName("ActDocumentId");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.MimeType).HasColumnName("MimeType");
            this.Property(t => t.Extension).HasColumnName("Extension");
        }
    }
}

using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities.Mapping
{
    public class SendToDocumentKindMap : EntityTypeConfiguration<SendToDocumentKind>
    {
        public SendToDocumentKindMap()
        {
            // Primary Key
            this.HasKey(t => t.SendToDocumentKindId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SendToDocumentKinds");
            this.Property(t => t.SendToDocumentKindId).HasColumnName("SendToDocumentKindId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

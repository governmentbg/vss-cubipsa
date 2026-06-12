using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
    }

    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Recipient)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Body)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Messages");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Recipient).HasColumnName("Recipient");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.IsBodyHtml).HasColumnName("IsBodyHtml");
            this.Property(t => t.SentDate).HasColumnName("SentDate");
        }
    }
}

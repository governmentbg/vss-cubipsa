using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Common.Portal.Model.Entities
{
    public partial class SystemLog
    {
        public int SystemLogId { get; set; }
        public string Level { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public string IP { get; set; }
        public string RawUrl { get; set; }
        public string Form { get; set; }
        public string UserAgent { get; set; }
        public string SessionId { get; set; }
        public Nullable<System.Guid> RequestId { get; set; }
        public string Message { get; set; }
    }

    public class SystemLogMap : EntityTypeConfiguration<SystemLog>
    {
        public SystemLogMap()
        {
            // Primary Key
            this.HasKey(t => t.SystemLogId);

            // Properties
            this.Property(t => t.Level)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IP)
                .HasMaxLength(50);

            this.Property(t => t.RawUrl)
                .HasMaxLength(500);

            this.Property(t => t.Form)
                .HasMaxLength(500);

            this.Property(t => t.UserAgent)
                .HasMaxLength(200);

            this.Property(t => t.SessionId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SystemLogs");
            this.Property(t => t.SystemLogId).HasColumnName("SystemLogId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.LogDate).HasColumnName("LogDate");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.RawUrl).HasColumnName("RawUrl");
            this.Property(t => t.Form).HasColumnName("Form");
            this.Property(t => t.UserAgent).HasColumnName("UserAgent");
            this.Property(t => t.SessionId).HasColumnName("SessionId");
            this.Property(t => t.RequestId).HasColumnName("RequestId");
            this.Property(t => t.Message).HasColumnName("Message");
        }
    }
}

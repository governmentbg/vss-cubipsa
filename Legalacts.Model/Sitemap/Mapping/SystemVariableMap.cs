using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Legalacts.Model.Sitemap.Entities.Mapping
{
    public class SystemVariableMap : EntityTypeConfiguration<SystemVariable>
    {
        public SystemVariableMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SystemVariables");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}

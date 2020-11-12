using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpLoad.Core.Models;

namespace SharpLoad.Infra.Data.Mapping
{
    public class LoadTestScriptMapping : IEntityTypeConfiguration<LoadTestScript>
    {
        public void Configure(EntityTypeBuilder<LoadTestScript> builder)
        {
            builder.ToTable("LoadTestScript");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
            builder.Property(x => x.BaseServerAddress).IsRequired();

            builder.HasMany(x => x.Requests).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}

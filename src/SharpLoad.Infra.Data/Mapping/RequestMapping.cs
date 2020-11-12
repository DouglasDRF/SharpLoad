using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpLoad.Core.Enums;
using SharpLoad.Core.Models;
using System;

namespace SharpLoad.Infra.Data.Mapping
{
    public class RequestMapping : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable("Request");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Body);
            builder.Property(x => x.Path).IsRequired();
            builder.Property(x => x.Method)
                .HasConversion(x => x.ToString(), value => (HttpMethods)Enum.Parse(typeof(HttpMethods), value))
                .IsRequired();

            builder.OwnsMany(x => x.Headers, h => {
                h.Property(x => x.Key).IsRequired();
                h.Property(x => x.Value).IsRequired();
            });
        }
    }
}

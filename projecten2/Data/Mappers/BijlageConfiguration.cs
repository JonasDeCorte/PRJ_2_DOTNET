using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten2.Data.Mappers
{
    public class BijlageConfiguration : IEntityTypeConfiguration<Bijlage>
    {
        public void Configure(EntityTypeBuilder<Bijlage> builder)
        {
            builder.ToTable("Bijlage");
            builder.HasKey(x => x.BijlageID);
            builder.Property(x => x.Omschrijving).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BestandType).IsRequired().HasMaxLength(50);
            
            
        }
    }
}
